using Application.Contracts.Persistence;
using Domain;

namespace Persistence.Repositories;

public class UserRepository(DataContext context) : GenericRepository<AppUser>(context), IUserRepository
{
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
