namespace Domain.Entities;

public class AdditionalService : Entity
{
    public AdditionalService()
    {
    }

    public AdditionalService(int id, string name, decimal dailyPrice)
        : base(id)
    {
        Name = name;
        DailyPrice = dailyPrice;
    }

    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
}