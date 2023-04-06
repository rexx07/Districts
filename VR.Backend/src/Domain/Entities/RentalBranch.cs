using Domain.Enums;

namespace Domain.Entities;

public class RentalBranch : Entity
{
    public RentalBranch()
    {
        Cars = new HashSet<Car>();
    }

    public RentalBranch(int id, City city)
        : this()
    {
        Id = id;
        City = city;
    }

    public City City { get; set; }

    public virtual ICollection<Car> Cars { get; set; }
}