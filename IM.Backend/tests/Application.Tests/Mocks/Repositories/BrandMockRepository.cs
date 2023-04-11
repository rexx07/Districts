using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Persistence.Repositories;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class BrandMockRepository : BaseMockRepository<IBrandRepository, Brand, MappingProfiles, BrandBusinessRules,
    BrandFakeData>
{
    public BrandMockRepository(BrandFakeData fakeData)
        : base(fakeData)
    {
    }
}