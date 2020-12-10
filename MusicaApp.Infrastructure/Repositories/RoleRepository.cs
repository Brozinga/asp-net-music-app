using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        public RoleRepository(SqliteContext db) : base(db)
        {
        }

        public async Task<IdentityRole> Add(IdentityRole entity)
        {
            var result = Db.Roles.Add(entity);
            return result.Entity;
        }

        public async Task<IdentityRole> Update(IdentityRole entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public void Delete(IdentityRole entity)
        {
            Db.Roles.Remove(entity);
        }
    }
}