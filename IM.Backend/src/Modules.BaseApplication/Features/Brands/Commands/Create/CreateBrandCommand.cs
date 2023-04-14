using AutoMapper;
using Core.Domain.Entities.Land;
using MediatR;
using Modules.BaseApplication.Features.Brands.Rules;
using Modules.BaseApplication.Pipelines.Authorization;
using Modules.BaseApplication.Pipelines.Caching;
using static Modules.BaseApplication.Features.Brands.Constants.BrandsOperationClaims;

namespace Modules.BaseApplication.Features.Brands.Commands.Create;

public class CreateBrandCommand : IRequest<CreatedBrandResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetBrands";

    public string[] Roles => new[] { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, Add };

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);

            Brand mappedBrand = _mapper.Map<Brand>(request);
            Brand createdBrand = await _brandRepository.AddAsync(mappedBrand);
            CreatedBrandResponse createdBrandResponse = _mapper.Map<CreatedBrandResponse>(createdBrand);
            return createdBrandResponse;
        }
    }
}