using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Models.Constants;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using static Modules.BaseApplication.Features.Models.Constants.ModelsOperationClaims;

namespace Modules.BaseApplication.Features.Models.Commands.Update;

public class UpdateModelCommand : IRequest<UpdatedModelResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetModels";

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, ModelsOperationClaims.Update };

    public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, UpdatedModelResponse>
    {
        public UpdateModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        private IModelRepository _modelRepository { get; }
        private IMapper _mapper { get; }

        public async Task<UpdatedModelResponse> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            Model mappedModel = _mapper.Map<Model>(request);
            Model updatedModel = await _modelRepository.UpdateAsync(mappedModel);
            UpdatedModelResponse updatedModelDto = _mapper.Map<UpdatedModelResponse>(updatedModel);
            return updatedModelDto;
        }
    }
}