﻿using Core.Domain.Entities.Land;

namespace Core.Domain.Entities;

public class IndividualCustomer : Entity
{
    public IndividualCustomer()
    {
    }

    public IndividualCustomer(int id, int customerId, string firstName, string lastName, string nationalIdentity)
        : base(id)
    {
        CustomerId = customerId;
        FirstName = firstName;
        LastName = lastName;
        NationalIdentity = nationalIdentity;
    }

    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalIdentity { get; set; }

    public virtual Customer Customer { get; set; }
}