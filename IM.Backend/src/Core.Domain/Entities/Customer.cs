﻿using Core.Domain.Entities.Land;
using Core.Domain.Entities.Security;

namespace Core.Domain.Entities;

public class Customer : Entity
{
    public Customer()
    {
        Invoices = new HashSet<Invoice>();
        Rentals = new HashSet<Rental>();
    }

    public Customer(int id, int userId)
        : this()
    {
        Id = id;
        UserId = userId;
    }

    public int UserId { get; set; }

    public virtual User User { get; set; }
    public virtual CorporateCustomer? CorporateCustomer { get; set; }
    public virtual FindeksCreditRate? FindeksCreditRate { get; set; }
    public virtual IndividualCustomer? IndividualCustomer { get; set; }
    public virtual ICollection<Invoice> Invoices { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }
}