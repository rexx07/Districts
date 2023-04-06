﻿namespace Domain.Entities;

public class OtpAuthenticator : Entity
{
    public OtpAuthenticator()
    {
    }

    public OtpAuthenticator(int id, int userId, byte[] secretKey, bool isVerified)
        : this()
    {
        Id = id;
        UserId = userId;
        SecretKey = secretKey;
        IsVerified = isVerified;
    }

    public int UserId { get; set; }
    public byte[] SecretKey { get; set; }
    public bool IsVerified { get; set; }

    public virtual User User { get; set; }
}