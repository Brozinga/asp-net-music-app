using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Domain.Models;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> FindUserByEmail(string email);
        Task<bool> Add(User entity, string password);
        Task<IList<string>> GetRoleOfUser(User entity);
        Task<User> GetAllMusicsWhereUser(string userId, int skip, int take);
    }
}