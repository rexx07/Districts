﻿using Core.Domain.Entities.Security;

namespace Modules.BaseApplication.Services.UserService;

public interface IUserService
{
    public Task<User?> GetByEmail(string email);
    public Task<User> GetById(int id);
    public Task<User> Update(User user);
}