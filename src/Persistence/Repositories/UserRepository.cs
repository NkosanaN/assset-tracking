﻿using Application.Contracts.Persistence;
using Application.User;
using Domain;

namespace Persistence.Repositories;

public class UserRepository : GenericRepository<AppUser>, IUserRepository
{
    public UserRepository(DataContext context) : base(context)
    { }
    public Task<IQueryable<AppUser>> GetAllUser()
    {
        return Task.FromResult(_dataContext.Users.AsQueryable());
    }

    //public Task<IQueryable<AppUserDto>> GetAllUser()
    //{
    //    var data =  _dataContext.Users.AsQueryable();

    //    return Task.FromResult(data);
    //}
}
