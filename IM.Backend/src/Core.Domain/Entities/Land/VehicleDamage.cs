namespace Core.Domain.Entities.Land;

public class VehicleDamage : Entity
{
    public VehicleDamage()
    {
    }

    public VehicleDamage(int id, int carId, string damageDescription, bool isFixed)
        : base(id)
    {
        CarId = carId;
        DamageDescription = damageDescription;
        IsFixed = isFixed;
    }

    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public virtual Vehicle? Car { get; set; }
}