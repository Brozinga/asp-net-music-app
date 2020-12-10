using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(SqliteContext db) : base(db)
        {
        }

        public async Task<User> Add(User entity)
        {
            var result = Db.Users.Add(entity);
            return result.Entity;
        }

        public async Task<User> Update(User entity)
        {
            var result = Db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(User entity)
        {
            Db.Users.Remove(entity);
        }
    }
}