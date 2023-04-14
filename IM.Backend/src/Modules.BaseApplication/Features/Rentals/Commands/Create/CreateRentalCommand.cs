using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using Core.Infrastructure.Mailing;
using MediatR;
using MimeKit;
using Modules.BaseApplication.Features.Rentals.Rules;
using Modules.BaseApplication.Pipelines.Logging;
using Modules.BaseApplication.Services.AdditionalServiceService;
using Modules.BaseApplication.Services.CarService;
using Modules.BaseApplication.Services.FindeksCreditRateService;
using Modules.BaseApplication.Services.InvoiceService;
using Modules.BaseApplication.Services.ModelService;
using Modules.BaseApplication.Services.POSService;
using Modules.BaseApplication.Services.RentalsIAdditionalServiceService;

namespace Modules.BaseApplication.Features.Rentals.Commands.Create;

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

            Vehicle? carToBeRented = await _carService.GetAvailableCarToRent(
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