using Application.Features.Transmissions.Rules;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Transmissions.Constants.TransmissionsOperationClaims;

namespace Application.Features.Transmissions.Commands.Create;

public class CreateTransmissionCommand : IRequest<CreatedTransmissionResponse>, ISecuredRequest
{
    public string Name { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class
        CreateTransmissionCommandHandler : IRequestHandler<CreateTransmissionCommand, CreatedTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;
        private readonly ITransmissionRepository _transmissionRepository;

        public CreateTransmissionCommandHandler(
            ITransmissionRepository transmissionRepository,
            IMapper mapper,
            TransmissionBusinessRules transmissionBusinessRules
        )
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
            _transmissionBusinessRules = transmissionBusinessRules;
        }

        public async Task<CreatedTransmissionResponse> Handle(CreateTransmissionCommand request,
                                                              CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionNameCanNotBeDuplicatedWhenInserted(request.Name);

            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission createdTransmission = await _transmissionRepository.AddAsync(mappedTransmission);
            CreatedTransmissionResponse createdTransmissionDto =
                _mapper.Map<CreatedTransmissionResponse>(createdTransmission);
            return createdTransmissionDto;
        }
    }
}