﻿namespace Core.Domain.Entities.Land;

public class Model : Entity
{
    public Model()
    {
        Cars = new HashSet<Vehicle>();
    }

    public Model(int id, int brandId, int fuelId, int transmissionId, string name, decimal dailyPrice, string imageUrl)
        : this()
    {
        Id = id;
        BrandId = brandId;
        FuelId = fuelId;
        TransmissionId = transmissionId;
        Name = name;
        DailyPrice = dailyPrice;
        ImageUrl = imageUrl;
    }

    public int BrandId { get; set; }
    public int FuelId { get; set; }
    public int TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }

    public virtual Brand? Brand { get; set; }
    public virtual ICollection<Vehicle> Cars { get; set; }
    public virtual Fuel? Fuel { get; set; }
    public virtual Transmission? Transmission { get; set; }
}