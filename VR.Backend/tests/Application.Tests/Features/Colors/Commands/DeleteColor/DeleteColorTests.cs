using System.Threading;
using System.Threading.Tasks;
using Application.Features.Colors.Commands.Delete;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Infrastructure.Common.Exceptions.Types;
using Xunit;

namespace Application.Tests.Features.Colors.Commands.DeleteColor;

public class DeleteColorTests : ColorMockRepository
{
    private readonly DeleteColorCommand _command;
    private readonly DeleteColorCommand.DeleteColorCommandHandler _handler;

    public DeleteColorTests(ColorFakeData fakeData, DeleteColorCommand command)
        : base(fakeData)
    {
        _command = command;
        _handler = new DeleteColorCommand.DeleteColorCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public async Task DeleteColorSuccessfully()
    {
        _command.Id = 1;
        DeletedColorResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ColorIdNotExistsShouldReturnError()
    {
        _command.Id = 6;
        await Assert.ThrowsAsync<BusinessException>(
            async () => await _handler.Handle(_command, CancellationToken.None));
    }
}