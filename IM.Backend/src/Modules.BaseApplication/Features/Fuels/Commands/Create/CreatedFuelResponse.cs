﻿using Application.Dtos;

namespace Application.Features.Fuels.Commands.Create;

public class CreatedFuelResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}