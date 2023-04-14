using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Brands.Constants;
using Modules.BaseApplication.Features.Brands.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using static Modules.BaseApplication.Features.Brands.Constants.BrandsOperationClaims;

namespace Modules.BaseApplication.Features.Brands.Commands.Update;

public class UpdateBrandCommand : IRequest<UpdatedBrandResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetBrands";

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, BrandsOperationClaims.Update };

    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, UpdatedBrandResponse>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<UpdatedBrandResponse> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(x => x.Id == request.Id);
            _brandBusinessRules.BrandIdShouldExistWhenSelected(brand);

            _mapper.Map(request, brand);
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenUpdated(brand);

            Brand updatedBrand = await _brandRepository.UpdateAsync(brand);
            UpdatedBrandResponse? response = _mapper.Map<UpdatedBrandResponse>(updatedBrand);
            return response;
        }
    }
}