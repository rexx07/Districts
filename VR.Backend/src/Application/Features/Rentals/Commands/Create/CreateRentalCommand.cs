﻿using Application.Features.Rentals.Rules;
using Application.Pipelines.Logging;
using Application.Services.AdditionalServiceService;
using Application.Services.CarService;
using Application.Services.FindeksCreditRateService;
using Application.Services.InvoiceService;
using Application.Services.ModelService;
using Application.Services.POSService;
using Application.Services.RentalsIAdditionalServiceService;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Mailing;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using MimeKit;

namespace Application.Features.Rentals.Commands.Create;

public class CreateRentalCommand : IRequest<CreatedRentalResponse>, ILoggableRequest
{
    public int ModelId { get; set; }
    public int CustomerId { get; set; }
    public DateTime RentStartDate { get; set; }
    public DateTime RentEndDate { get; set; }
    public int RentStartRentalBranchId { get; set; }
    public int RentEndRentalBranchId { get; set; }
    public int[] AdditionalServiceIds { get; set; }

    public class CreateRentalCommandHandler : IRequestHandler<CreateRentalCommand, CreatedRentalResponse>
    {
        private readonly IAdditionalServiceService _additionalServiceService;
        private readonly ICarService _carService;
        private readonly IFindeksCreditRateService _findeksCreditRateService;
        private readonly IInvoiceService _invoiceService;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IModelService _modelService;
        private readonly IPOSService _posService;
        private readonly RentalBusinessRules _rentalBusinessRules;
        private readonly IRentalRepository _rentalRepository;
        private readonly IRentalsAdditionalServiceService _rentalsAdditionalServiceService;

        public CreateRentalCommandHandler(
            IRentalRepository rentalRepository,
            IMapper mapper,
            RentalBusinessRules rentalBusinessRules,
            IAdditionalServiceService additionalServiceService,
            ICarService carService,
            IFindeksCreditRateService findeksCreditRateService,
            IInvoiceService invoiceService,
            IModelService modelService,
            IMailService mailService,
            IPOSService posService,
            IRentalsAdditionalServiceService rentalsAdditionalServiceService
        )
        {
            _rentalRepository = rentalRepository;
            _mapper = mapper;
            _rentalBusinessRules = rentalBusinessRules;
            _additionalServiceService = additionalServiceService;
            _carService = carService;
            _findeksCreditRateService = findeksCreditRateService;
            _invoiceService = invoiceService;
            _modelService = modelService;
            _mailService = mailService;
            _posService = posService;
            _rentalsAdditionalServiceService = rentalsAdditionalServiceService;
        }

        public async Task<CreatedRentalResponse> Handle(CreateRentalCommand request,
                                                        CancellationToken cancellationToken)
        {
            FindeksCreditRate customerFindeksCreditRate =
                await _findeksCreditRateService.GetFindeksCreditRateByCustomerId(
                    request.CustomerId
                );

            Car? carToBeRented = await _carService.GetAvailableCarToRent(
                                     request.ModelId,
                                     request.RentStartRentalBranchId,
                                     request.RentStartDate,
                                     request.RentEndDate
                                 );

            await _rentalBusinessRules.RentalCanNotBeCreatedWhenCustomerFindeksScoreLowerThanCarMinFindeksScore(
                customerFindeksCreditRate.Score,
                carToBeRented.MinFindeksCreditRate
            );

            Model model = await _modelService.GetById(carToBeRented.ModelId);

            Rental mappedRental = _mapper.Map<Rental>(request);
            mappedRental.CarId = carToBeRented.Id;
            //mappedRental.RentStartRentalBranchId = carToBeRented.RentalBranchId;
            mappedRental.RentStartKilometer = carToBeRented.Kilometer;

            IList<AdditionalService> additionalServices =
                await _additionalServiceService.GetListByIds(request.AdditionalServiceIds);
            decimal totalAdditionalServicesPrice = additionalServices.Sum(a => a.DailyPrice);

            decimal dailyPrice = model.DailyPrice + totalAdditionalServicesPrice;
            Invoice newInvoice = await _invoiceService.CreateInvoice(mappedRental, dailyPrice);

            await _posService.Pay(newInvoice.No, newInvoice.RentalPrice);

            await _invoiceService.Add(newInvoice);

            Rental createdRental = await _rentalRepository.AddAsync(mappedRental);
            await _rentalsAdditionalServiceService.AddManyByRentalIdAndAdditionalServices(
                createdRental.Id, additionalServices);

            var toEmailList = new List<MailboxAddress>
                { new(name: "Ahmet Çetinkaya", address: "ahmetcetinkaya7@outlook.com") };

            _mailService.SendMail(
                new Mail
                {
                    Subject = "New Rental",
                    TextBody = "A rental has been created.",
                    ToList = toEmailList
                }
            );

            CreatedRentalResponse createdRentalDto = _mapper.Map<CreatedRentalResponse>(createdRental);
            return createdRentalDto;
        }
    }
}