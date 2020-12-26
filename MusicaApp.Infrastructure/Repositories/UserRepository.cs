using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;

namespace MusicApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
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
    }
}