using Application.Features.FindeksCreditRates.Rules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.Create;

public class CreateFindeksCreditRateCommand : IRequest<CreatedFindeksCreditRateResponse>
{
    public int CustomerId { get; set; }
    public short Score { get; set; }

    public class
        CreateFindeksCreditRateCommandHandler : IRequestHandler<CreateFindeksCreditRateCommand,
            CreatedFindeksCreditRateResponse>
    {
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public CreateFindeksCreditRateCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IMapper mapper,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<CreatedFindeksCreditRateResponse> Handle(
            CreateFindeksCreditRateCommand request,
            CancellationToken cancellationToken
        )
        {
            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate createdFindeksCreditRate =
                await _findeksCreditRateRepository.AddAsync(mappedFindeksCreditRate);
            CreatedFindeksCreditRateResponse createdFindeksCreditRateDto =
                _mapper.Map<CreatedFindeksCreditRateResponse>(
                    createdFindeksCreditRate
                );
            return createdFindeksCreditRateDto;
        }
    }
}