﻿using System.Threading;
using System.Threading.Tasks;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Test.Application.Constants;
using FluentValidation.Results;
using Xunit;

namespace Application.Tests.Features.Colors.Commands.UpdateColor;

public class UpdateColorTests : ColorMockRepository
{
    private readonly UpdateColorCommand _command;
    private readonly UpdateColorCommandHandler _handler;
    private readonly UpdateColorCommandValidator _validator;

    public UpdateColorTests(ColorFakeData fakeData, UpdateColorCommandValidator validator, UpdateColorCommand command)
        : base(fakeData)
    {
        _validator = validator;
        _command = command;
        _handler = new UpdateColorCommandHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    public void ColordNameEmptyShouldReturnError()
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
    public async Task UpdateColorShouldSuccessfully()
    {
        _command.Id = 1;
        _command.Name = "Pink";
        UpdatedColorResponse result = await _handler.Handle(_command, CancellationToken.None);
        Assert.Equal(expected: "Pink", result.Name);
    }

    [Fact]
    public async Task ColorIdNotExistsShouldReturnError()
    {
        _command.Id = 6;
        _command.Name = "Pink";
        await Assert.ThrowsAsync<BusinessException>(
            async () => await _handler.Handle(_command, CancellationToken.None));
    }
}