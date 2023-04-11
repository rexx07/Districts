using Application.Tests.Mocks.FakeData;
using Core.Domain.Entities;
using Core.Persistence.Repositories;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class ColorMockRepository : BaseMockRepository<IColorRepository, Color, MappingProfiles, ColorBusinessRules,
    ColorFakeData>
{
    public ColorMockRepository(ColorFakeData fakeData)
        : base(fakeData)
    {
    }
}