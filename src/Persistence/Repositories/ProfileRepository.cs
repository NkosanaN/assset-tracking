//using Application.Contracts.Persistence;
//using Application.Profiles;
//using Microsoft.EntityFrameworkCore;

//namespace Persistence.Repositories;

//public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
//{
//    public ProfileRepository(DataContext context) : base(context)
//    {

//    }

//    public async Task<User> GetUserProfile(string name)
//    {
//        return await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == name);
//    }
//}

