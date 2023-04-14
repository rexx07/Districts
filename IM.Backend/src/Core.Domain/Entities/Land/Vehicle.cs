using Core.Domain.Entities.BaseEntities;
using Core.Domain.Enums;

namespace Core.Domain.Entities.Land;

public record Vehicle : Aggregate<long>
{
    public Vehicle()
    {
        Rentals = new HashSet<Rental>();
        CarDamages = new HashSet<VehicleDamage>();
    }

    public Vehicle(
        int id,
        int colorId,
        int modelId,
        int rentalBranchId,
        VehicleState carState,
        int kilometer,
        short modelYear,
        string plate,
        short minFindeksCreditRate,
        VehicleType vehicleType
    )
        : this()
    {
        Id = id;
        ColorId = colorId;
        ModelId = modelId;
        RentalBranchId = rentalBranchId;
        CarState = carState;
        Kilometer = kilometer;
        ModelYear = modelYear;
        Plate = plate;
        MinFindeksCreditRate = minFindeksCreditRate;
        VehicleType = vehicleType;
    }

    public int ColorId { get; set; }
    public int ModelId { get; set; }
    public VehicleType VehicleType { get; set; }
    public int RentalBranchId { get; set; }
    public VehicleState CarState { get; set; }
    public int Kilometer { get; set; }
    public short ModelYear { get; set; }
    public string Plate { get; set; }
    public short MinFindeksCreditRate { get; set; }

    public virtual Color? Color { get; set; }
    public virtual RentalBranch? RentalBranch { get; set; }
    public virtual Model? Model { get; set; }
    public virtual ICollection<VehicleDamage> CarDamages { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }
}