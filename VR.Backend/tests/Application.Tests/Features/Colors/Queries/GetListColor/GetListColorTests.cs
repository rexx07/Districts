using System.Threading;
using System.Threading.Tasks;
using Application.Features.Colors.Queries.GetList;
using Application.Requests;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Infrastructure.Persistence.Paging;
using Xunit;

namespace Application.Tests.Features.Colors.Queries.GetListColor;

public class GetListColorTests : ColorMockRepository
{
    private readonly GetListColorQuery.GetListColorQueryHandler _handler;
    private readonly GetListColorQuery _query;

    public GetListColorTests(ColorFakeData fakeData, GetListColorQuery query)
        : base(fakeData)
    {
        _query = query;
        _handler = new GetListColorQuery.GetListColorQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    public async Task GetAllColorsShouldSuccessfuly()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 3 };
        GetListResponse<GetListColorListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, result.Items.Count);
    }
}