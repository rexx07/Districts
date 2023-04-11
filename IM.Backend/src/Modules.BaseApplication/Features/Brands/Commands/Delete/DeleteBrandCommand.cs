﻿using Application.Features.Brands.Constants;
using Application.Features.Brands.Rules;
using Application.Pipelines.Authorization;
using Application.Pipelines.Caching;
using AutoMapper;
using Core.Domain.Entities;
using Core.Domain.Entities.Land;
using MediatR;
using static Application.Features.Brands.Constants.BrandsOperationClaims;

namespace Application.Features.Brands.Commands.Delete;

public class DeleteBrandCommand : IRequest<DeletedBrandResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetBrands";

    public string[] Roles => new[]
        { Core.Domain.Constants.OperationClaims.Admin, Admin, Write, BrandsOperationClaims.Delete };

    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, DeletedBrandResponse>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper,
                                         BrandBusinessRules brandBusinessRules)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<DeletedBrandResponse> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(x => x.Id == request.Id);
            _brandBusinessRules.BrandIdShouldExistWhenSelected(brand);

            _mapper.Map(request, brand);
            Brand deletedBrand = await _brandRepository.DeleteAsync(brand);

            DeletedBrandResponse? response = _mapper.Map<DeletedBrandResponse>(deletedBrand);
            return response;
        }
    }
}