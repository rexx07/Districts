using Core.Domain.Enums;
using Modules.BaseApplication.Dtos;

namespace Modules.BaseApplication.Features.Cars.Queries.GetListByDynamic;

public class GetListByDynamicCarListItemDto : IDto
{
    public int Id { get; set; }
    public string BrandName { get; set; }
    public string ModelName { get; set; }
    public string ColorName { get; set; }
    public string Plate { get; set; }
    public VehicleState CarState { get; set; }
    public short ModelYear { get; set; }
}