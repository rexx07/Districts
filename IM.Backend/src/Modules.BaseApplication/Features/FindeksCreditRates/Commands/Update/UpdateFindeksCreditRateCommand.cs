using Application.Features.FindeksCreditRates.Rules;
using AutoMapper;
using Core.Domain.Entities;
using MediatR;

namespace Application.Features.FindeksCreditRates.Commands.Update;

public class UpdateFindeksCreditRateCommand : IRequest<UpdatedFindeksCreditRateResponse>
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public short Score { get; set; }

    public class
        UpdateFindeksCreditRateCommandHandler : IRequestHandler<UpdateFindeksCreditRateCommand,
            UpdatedFindeksCreditRateResponse>
    {
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public UpdateFindeksCreditRateCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IMapper mapper,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<UpdatedFindeksCreditRateResponse> Handle(
            UpdateFindeksCreditRateCommand request,
            CancellationToken cancellationToken
        )
        {
            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate updatedFindeksCreditRate =
                await _findeksCreditRateRepository.UpdateAsync(mappedFindeksCreditRate);
            UpdatedFindeksCreditRateResponse updatedFindeksCreditRateDto =
                _mapper.Map<UpdatedFindeksCreditRateResponse>(
                    updatedFindeksCreditRate
                );
            return updatedFindeksCreditRateDto;
        }
    }
}