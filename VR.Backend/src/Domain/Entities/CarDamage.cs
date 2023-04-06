namespace Domain.Entities;

public class CarDamage : Entity
{
    public CarDamage()
    {
    }

    public CarDamage(int id, int carId, string damageDescription, bool isFixed)
        : base(id)
    {
        CarId = carId;
        DamageDescription = damageDescription;
        IsFixed = isFixed;
    }

    public int CarId { get; set; }
    public string DamageDescription { get; set; }
    public bool IsFixed { get; set; }

    public virtual Car? Car { get; set; }
}