namespace Domain.Entities;

public class Entity
{
    public Entity()
    {
    }

    public Entity(int id)
        : this()
    {
        Id = id;
    }

    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}