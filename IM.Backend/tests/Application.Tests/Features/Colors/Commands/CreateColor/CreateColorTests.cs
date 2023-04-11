using System.Threading;
using System.Threading.Tasks;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;

namespace Application.Tests.Features.Colors.Commands.CreateColor;

public class CreateColorTests : ColorMockRepository
{
    private readonly CreateColorCommand _command;
    private readonly CreateColorCommandHandler _handler;
    private readonly CreateColorCommandValidator _validator;

    public CreateColorTests(ColorFakeData fakeData, CreateColorCommandValidator validator, CreateColorCommand command)
        : base(fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new CreateColorCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void ColorNameEmptyShouldReturnError()
    {
        _command.Name = string.Empty;
        ValidationFailure? result = _validator
                                    .Validate(_command)
                                    .Errors.Where(x => x.PropertyName == "Name" &&
                                                       x.ErrorCode == ValidationErrorCodes.NotEmptyValidator)
                                    .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.NotEmptyValidator, result?.ErrorCode);
    }

    [Fact]
    public void ColorNameNotMatchMinLenghtRuleShouldReturnError()
    {
        _command.Name = "P";
        ValidationFailure? result = _validator
                                    .Validate(_command)
                                    .Errors.Where(x => x.PropertyName == "Name" &&
                                                       x.ErrorCode == ValidationErrorCodes.MinimumLengthValidator)
                                    .FirstOrDefault();
        Assert.Equal(ValidationErrorCodes.MinimumLengthValidator, result?.ErrorCode);
    }

    [Fact]
    public async Task CreateColorShouldSuccessfully()
    {
        _command.Name = "Pink";
        CreatedColorResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: "Pink", result.Name);
    }

    [Fact]
    public async Task DuplicatedColorNameShouldReturnError()
    {
        _command.Name = "Blue";
        await Assert.ThrowsAsync<BusinessException>(
            async () => await _handler.Handle(_command, CancellationToken.None));
    }
}