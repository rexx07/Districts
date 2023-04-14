using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Fuels.Queries.GetById;

public class GetByIdFuelResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}