using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Transmissions.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using static Modules.BaseApplication.Features.Transmissions.Constants.TransmissionsOperationClaims;

namespace Modules.BaseApplication.Features.Transmissions.Commands.Delete;

public class DeleteTransmissionCommand : IRequest<DeletedTransmissionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, TransmissionsOperationClaims.Delete };

    public class
        DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, DeletedTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITransmissionRepository _transmissionRepository;

        public DeleteTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<DeletedTransmissionResponse> Handle(DeleteTransmissionCommand request,
                                                              CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission deletedTransmission = await _transmissionRepository.DeleteAsync(mappedTransmission);
            DeletedTransmissionResponse deletedTransmissionDto =
                _mapper.Map<DeletedTransmissionResponse>(deletedTransmission);
            return deletedTransmissionDto;
        }
    }
}