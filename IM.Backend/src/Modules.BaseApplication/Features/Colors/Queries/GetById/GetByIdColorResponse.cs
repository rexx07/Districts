using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Colors.Queries.GetById;

public class GetByIdColorResponse : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}