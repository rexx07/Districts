using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.AdditionalServices.Constants;
using Modules.BaseApplication.Features.AdditionalServices.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.AdditionalServices.Constants.AdditionalServicesOperationClaims;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Update;

public class UpdateAdditionalServiceCommand : IRequest<UpdatedAdditionalServiceResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, AdditionalServicesOperationClaims.Update };

    public class
        UpdateAdditionalServiceCommandHandler : IRequestHandler<UpdateAdditionalServiceCommand,
            UpdatedAdditionalServiceResponse>
    {
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;

        public UpdateAdditionalServiceCommandHandler(
            IAdditionalServiceRepository additionalServiceRepository,
            IMapper mapper,
            AdditionalServiceBusinessRules additionalServiceBusinessRules
        )
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<UpdatedAdditionalServiceResponse> Handle(
            UpdateAdditionalServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService updatedAdditionalService =
                await _additionalServiceRepository.UpdateAsync(mappedAdditionalService);
            UpdatedAdditionalServiceResponse updatedAdditionalServiceResponse =
                _mapper.Map<UpdatedAdditionalServiceResponse>(
                    updatedAdditionalService
                );
            return updatedAdditionalServiceResponse;
        }
    }
}