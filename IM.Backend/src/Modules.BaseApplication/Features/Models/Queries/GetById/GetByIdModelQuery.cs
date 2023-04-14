using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Models.Rules;

namespace Modules.BaseApplication.Features.Models.Queries.GetById;

public class GetByIdModelQuery : IRequest<GetByIdModelResponse>
{
    public int Id { get; set; }

    public class GetByIdModelQueryHandler : IRequestHandler<GetByIdModelQuery, GetByIdModelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ModelBusinessRules _modelBusinessRules;
        private readonly IModelRepository _modelRepository;

        public GetByIdModelQueryHandler(IModelRepository modelRepository, ModelBusinessRules modelBusinessRules,
                                        IMapper mapper)
        {
            _modelRepository = modelRepository;
            _modelBusinessRules = modelBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdModelResponse> Handle(GetByIdModelQuery request, CancellationToken cancellationToken)
        {
            await _modelBusinessRules.ModelIdShouldExistWhenSelected(request.Id);

            Model? model = await _modelRepository.GetAsync(m => m.Id == request.Id);
            GetByIdModelResponse modelDto = _mapper.Map<GetByIdModelResponse>(model);
            return modelDto;
        }
    }
}