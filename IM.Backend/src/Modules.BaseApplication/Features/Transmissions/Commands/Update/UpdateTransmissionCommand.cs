using Application.Features.Transmissions.Constants;
using Application.Pipelines.Authorization;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using MediatR;
using static Application.Features.Transmissions.Constants.TransmissionsOperationClaims;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdateTransmissionCommand : IRequest<UpdatedTransmissionResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, TransmissionsOperationClaims.Update };

    public class
        UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, UpdatedTransmissionResponse>
    {
        public UpdateTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        private ITransmissionRepository _transmissionRepository { get; }
        private IMapper _mapper { get; }

        public async Task<UpdatedTransmissionResponse> Handle(UpdateTransmissionCommand request,
                                                              CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission updatedTransmission = await _transmissionRepository.UpdateAsync(mappedTransmission);
            UpdatedTransmissionResponse updatedTransmissionDto =
                _mapper.Map<UpdatedTransmissionResponse>(updatedTransmission);
            return updatedTransmissionDto;
        }
    }
}