using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager, SqliteContext db) :base(db)
        {
            _userManager = userManager;
        }

        public async Task<bool> Add(User entity)
        {
            var result = await _userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Add(User entity, string password)
        {
            var result = await _userManager.CreateAsync(entity, password);
            return result.Succeeded;
        }

        public async Task<bool> Update(User entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(User entity)
        {
            var result = await _userManager.DeleteAsync(entity);
            return result.Succeeded;
        }

        public async Task<IList<string>> GetRoleOfUser(User entity)
        {
            var result = await _userManager.GetRolesAsync(entity);
            return result;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

        public async Task<User> GetAllMusicsWhereUser(string userId, int skip, int take)
        {
            var result = await Db.Users
                .Include(x => x.MusicsToUsers)
                .ThenInclude(x => x.Music)
                .FirstOrDefaultAsync(x => x.Id == userId);


            //--- Recebi o erro dizendo que não funciona no SQLite, tenho que testar em outra base.
            //var result = await Db.Users
            //    .Include(x => x.MusicsToUsers.Skip(skip).Take(take))
            //    .ThenInclude(x => x.Music)
            //    .FirstOrDefaultAsync(x => x.Id == userId);


            //--- Funciona mas fica muito verboso para o Repository assim fica melhor usando o AutoMapper
            //var result = await Db.Users
            //    .Include(x => x.MusicsToUsers)
            //    .ThenInclude(x => x.Music)
            //    .Select(x => new MusicsResponse
            //    {
            //        Id = x.Id,
            //        UserName = x.UserName,
            //        Email = x.Email,
            //        Musics = x.MusicsToUsers.Select(x => x.Music).ToList()
            //    })
            //    .FirstOrDefaultAsync(x => x.Id == userId);

            return result;
        }
    }
}