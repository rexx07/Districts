using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.FindeksCreditRates.Constants;
using Modules.BaseApplication.Features.FindeksCreditRates.Rules;
using Modules.BaseApplication.Pipelines.Authorization;

namespace Modules.BaseApplication.Features.FindeksCreditRates.Commands.Delete;

public class DeleteFindeksCreditRateCommand : IRequest<DeletedFindeksCreditRateResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles =>
        new[] { FindeksCreditRatesOperationClaims.Admin, FindeksCreditRatesOperationClaims.Delete };

    public class
        DeleteFindeksCreditRateCommandHandler : IRequestHandler<DeleteFindeksCreditRateCommand,
            DeletedFindeksCreditRateResponse>
    {
        private readonly FindeksCreditRateBusinessRules _findeksCreditRateBusinessRules;
        private readonly IFindeksCreditRateRepository _findeksCreditRateRepository;
        private readonly IMapper _mapper;

        public DeleteFindeksCreditRateCommandHandler(
            IFindeksCreditRateRepository findeksCreditRateRepository,
            IMapper mapper,
            FindeksCreditRateBusinessRules findeksCreditRateBusinessRules
        )
        {
            _findeksCreditRateRepository = findeksCreditRateRepository;
            _mapper = mapper;
            _findeksCreditRateBusinessRules = findeksCreditRateBusinessRules;
        }

        public async Task<DeletedFindeksCreditRateResponse> Handle(
            DeleteFindeksCreditRateCommand request,
            CancellationToken cancellationToken
        )
        {
            await _findeksCreditRateBusinessRules.FindeksCreditRateIdShouldExistWhenSelected(request.Id);

            FindeksCreditRate mappedFindeksCreditRate = _mapper.Map<FindeksCreditRate>(request);
            FindeksCreditRate deletedFindeksCreditRate =
                await _findeksCreditRateRepository.DeleteAsync(mappedFindeksCreditRate);
            DeletedFindeksCreditRateResponse deletedFindeksCreditRateDto =
                _mapper.Map<DeletedFindeksCreditRateResponse>(
                    deletedFindeksCreditRate
                );
            return deletedFindeksCreditRateDto;
        }
    }
}