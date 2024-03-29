﻿using System.Threading;
using System.Threading.Tasks;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;

namespace Application.Tests.Features.Brands.Queries.GetByIdBrand;

public class GetByIdBrandTests : BrandMockRepository
{
    private readonly GetByIdBrandQueryHandler _handler;
    private readonly GetByIdBrandQuery _query;

    public GetByIdBrandTests(BrandFakeData fakeData, GetByIdBrandQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdBrandQueryHandler(MockRepository.Object, BusinessRules, Mapper);
    }

    [Fact]
    public async Task GetByIdBrandShouldSuccessfully()
    {
        _query.Id = 1;
        GetByIdBrandResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: "Mercedes", result.Name);
    }

    [Fact]
    public async Task BrandIdNotExistsShouldReturnError()
    {
        _query.Id = 6;
        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
    }
}