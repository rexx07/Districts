using System.Collections.Generic;
using Core.Domain.Entities;
using Core.Test.Application.FakeData;

namespace Application.Tests.Mocks.FakeData;

public class BrandFakeData : BaseFakeData<Brand>
{
    public override List<Brand> CreateFakeData()
    {
        var data = new List<Brand>
        {
            new() { Id = 1, Name = "Mercedes" },
            new() { Id = 2, Name = "BMW" }
        };
        return data;
    }
}