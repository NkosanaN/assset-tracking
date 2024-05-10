using Application.Profiles;

namespace Application.Contracts.Persistence;
public interface IProfileRepository : IGenericRepository<Profile>
{
    Task<Profile> GetUserProfile(string name);
}
