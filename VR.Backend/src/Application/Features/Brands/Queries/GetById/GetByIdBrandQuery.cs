using Application.Features.Brands.Rules;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;
using MediatR;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandResponse>
{
    public int Id { get; set; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandResponse>
    {
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public GetByIdBrandQueryHandler(IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules,
                                        IMapper mapper)
        {
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
            _mapper = mapper;
        }

        public async Task<GetByIdBrandResponse> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(x => x.Id == request.Id);
            _brandBusinessRules.BrandIdShouldExistWhenSelected(brand);

            GetByIdBrandResponse? response = _mapper.Map<GetByIdBrandResponse>(brand);
            return response;
        }
    }
}