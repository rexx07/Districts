using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.AdditionalServices.Constants;
using Modules.BaseApplication.Features.AdditionalServices.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.AdditionalServices.Constants.AdditionalServicesOperationClaims;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Delete;

public class DeleteAdditionalServiceCommand : IRequest<DeletedAdditionalServiceResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, AdditionalServicesOperationClaims.Delete };

    public class
        DeleteAdditionalServiceCommandHandler : IRequestHandler<DeleteAdditionalServiceCommand,
            DeletedAdditionalServiceResponse>
    {
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;

        public DeleteAdditionalServiceCommandHandler(
            IAdditionalServiceRepository additionalServiceRepository,
            IMapper mapper,
            AdditionalServiceBusinessRules additionalServiceBusinessRules
        )
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<DeletedAdditionalServiceResponse> Handle(
            DeleteAdditionalServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            await _additionalServiceBusinessRules.AdditionalServiceIdShouldExistWhenSelected(request.Id);

            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService deletedAdditionalService =
                await _additionalServiceRepository.DeleteAsync(mappedAdditionalService);
            DeletedAdditionalServiceResponse deletedAdditionalServiceResponse =
                _mapper.Map<DeletedAdditionalServiceResponse>(
                    deletedAdditionalService
                );
            return deletedAdditionalServiceResponse;
        }
    }
}