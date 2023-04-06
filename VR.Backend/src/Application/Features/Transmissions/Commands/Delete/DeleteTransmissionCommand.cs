using Application.Features.Transmissions.Constants;
using Application.Pipelines.Authorization;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Transmissions.Constants.TransmissionsOperationClaims;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeleteTransmissionCommand : IRequest<DeletedTransmissionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[]
        { Domain.Constants.OperationClaims.Admin, Admin, Write, TransmissionsOperationClaims.Delete };

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