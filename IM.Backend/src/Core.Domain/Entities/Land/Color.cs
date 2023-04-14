namespace Core.Domain.Entities.Land;

public class Color : Entity
{
    public Color()
    {
        Cars = new HashSet<Vehicle>();
    }

    public Color(int id, string name)
        : this()
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }

    public virtual ICollection<Vehicle> Cars { get; set; }
}