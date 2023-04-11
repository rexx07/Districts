using Core.Domain.Entities.Land;

namespace Core.Domain.Entities.Security;

public class OperationClaim : Entity
{
    public OperationClaim()
    {
    }

    public OperationClaim(int id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }
}