using Domain;

namespace Application.Contracts.Persistence;
public interface IUserRepository : IGenericRepository<AppUser>
{
    Task<IQueryable<AppUser>> GetAllUser();
}
