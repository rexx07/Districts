using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Colors.Rules;

namespace Modules.BaseApplication.Features.Colors.Queries.GetById;

public class GetByIdColorQuery : IRequest<GetByIdColorResponse>
{
    public int Id { get; set; }

    public class GetByIdColorQueryHandler : IRequestHandler<GetByIdColorQuery, GetByIdColorResponse>
    {
        private readonly ColorBusinessRules _colorBusinessRules;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public GetByIdColorQueryHandler(IColorRepository colorRepository, ColorBusinessRules colorBusinessRules,
                                        IMapper mapper)
        {
            _colorRepository = colorRepository;
            _colorBusinessRules = colorBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdColorResponse> Handle(GetByIdColorQuery request, CancellationToken cancellationToken)
        {
            await _colorBusinessRules.ColorIdShouldExistWhenSelected(request.Id);

            Color? color = await _colorRepository.GetAsync(c => c.Id == request.Id);
            GetByIdColorResponse colorDto = _mapper.Map<GetByIdColorResponse>(color);
            return colorDto;
        }
    }
}