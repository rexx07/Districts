using AutoMapper;
using Core.Domain.Entities;
using MediatR;
using Modules.BaseApplication.Features.AdditionalServices.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.AdditionalServices.Constants.AdditionalServicesOperationClaims;

namespace Modules.BaseApplication.Features.AdditionalServices.Commands.Create;

public class CreateAdditionalServiceCommand : IRequest<CreatedAdditionalServiceResponse>, ISecuredRequest
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class
        CreateAdditionalServiceCommandHandler : IRequestHandler<CreateAdditionalServiceCommand,
            CreatedAdditionalServiceResponse>
    {
        private readonly AdditionalServiceBusinessRules _additionalServiceBusinessRules;
        private readonly IAdditionalServiceRepository _additionalServiceRepository;
        private readonly IMapper _mapper;

        public CreateAdditionalServiceCommandHandler(
            IAdditionalServiceRepository additionalServiceRepository,
            IMapper mapper,
            AdditionalServiceBusinessRules additionalServiceBusinessRules
        )
        {
            _additionalServiceRepository = additionalServiceRepository;
            _mapper = mapper;
            _additionalServiceBusinessRules = additionalServiceBusinessRules;
        }

        public async Task<CreatedAdditionalServiceResponse> Handle(
            CreateAdditionalServiceCommand request,
            CancellationToken cancellationToken
        )
        {
            await _additionalServiceBusinessRules.AdditionalServiceNameCanNotBeDuplicatedWhenInserted(request.Name);

            AdditionalService mappedAdditionalService = _mapper.Map<AdditionalService>(request);
            AdditionalService createdAdditionalService =
                await _additionalServiceRepository.AddAsync(mappedAdditionalService);
            CreatedAdditionalServiceResponse createdAdditionalServiceResponse =
                _mapper.Map<CreatedAdditionalServiceResponse>(
                    createdAdditionalService
                );
            return createdAdditionalServiceResponse;
        }
    }
}