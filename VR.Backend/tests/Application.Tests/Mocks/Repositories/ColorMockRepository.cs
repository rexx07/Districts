using Application.Features.Colors.Profiles;
using Application.Features.Colors.Rules;
using Application.Tests.Mocks.FakeData;
using Core.Test.Application.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.RepositoryContracts;

namespace Application.Tests.Mocks.Repositories;

public class ColorMockRepository : BaseMockRepository<IColorRepository, Color, MappingProfiles, ColorBusinessRules,
    ColorFakeData>
{
    public ColorMockRepository(ColorFakeData fakeData)
        : base(fakeData)
    {
    }
}