using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Models.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using static Modules.BaseApplication.Features.Models.Constants.ModelsOperationClaims;

namespace Modules.BaseApplication.Features.Models.Commands.Delete;

public class DeleteModelCommand : IRequest<DeletedModelResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetModels";

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, ModelsOperationClaims.Delete };

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, DeletedModelResponse>
    {
        private readonly IMapper _mapper;
        private readonly IModelRepository _modelRepository;

        public DeleteModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<DeletedModelResponse> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model deletedModel = await _modelRepository.DeleteAsync(mappedModel);
            DeletedModelResponse deletedModelDto = _mapper.Map<DeletedModelResponse>(deletedModel);
            return deletedModelDto;
        }
    }
}