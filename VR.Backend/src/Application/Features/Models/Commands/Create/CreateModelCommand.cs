using Application.Features.Models.Rules;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;
using static Application.Features.Models.Constants.ModelsOperationClaims;

namespace Application.Features.Models.Commands.Create;

public class CreateModelCommand : IRequest<CreatedModelResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public int BrandId { get; set; }
    public int TransmissionId { get; set; }
    public int FuelId { get; set; }
    public string ImageUrl { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetModels";

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, CreatedModelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ModelBusinessRules _modelBusinessRules;
        private readonly IModelRepository _modelRepository;

        public CreateModelCommandHandler(IModelRepository modelRepository, IMapper mapper,
                                         ModelBusinessRules modelBusinessRules)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            _modelBusinessRules = modelBusinessRules;
        }

        public async Task<CreatedModelResponse> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model createdModel = await _modelRepository.AddAsync(mappedModel);
            CreatedModelResponse createdModelDto = _mapper.Map<CreatedModelResponse>(createdModel);
            return createdModelDto;
        }
    }
}