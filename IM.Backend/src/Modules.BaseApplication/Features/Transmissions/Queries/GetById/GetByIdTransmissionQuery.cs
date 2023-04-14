using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Transmissions.Rules;

namespace Modules.BaseApplication.Features.Transmissions.Queries.GetById;

public class GetByIdTransmissionQuery : IRequest<GetByIdTransmissionResponse>
{
    public int Id { get; set; }

    public class
        GetByIdTransmissionQueryHandler : IRequestHandler<GetByIdTransmissionQuery, GetByIdTransmissionResponse>
    {
        private readonly IMapper _mapper;
        private readonly TransmissionBusinessRules _transmissionBusinessRules;
        private readonly ITransmissionRepository _transmissionRepository;

        public GetByIdTransmissionQueryHandler(
            ITransmissionRepository transmissionRepository,
            TransmissionBusinessRules transmissionBusinessRules,
            IMapper mapper
        )
        {
            _transmissionRepository = transmissionRepository;
            _transmissionBusinessRules = transmissionBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdTransmissionResponse> Handle(GetByIdTransmissionQuery request,
                                                              CancellationToken cancellationToken)
        {
            await _transmissionBusinessRules.TransmissionIdShouldExistWhenSelected(request.Id);

            Transmission? transmission = await _transmissionRepository.GetAsync(t => t.Id == request.Id);
            GetByIdTransmissionResponse transmissionDto = _mapper.Map<GetByIdTransmissionResponse>(transmission);
            return transmissionDto;
        }
    }
}