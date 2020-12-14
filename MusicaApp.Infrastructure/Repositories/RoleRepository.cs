using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicApp.Domain.Interfaces.Repositories;

namespace MusicApp.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Add(IdentityRole entity)
        {
            var result = await _roleManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Update(IdentityRole entity)
        {
            var result = await _roleManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(IdentityRole entity)
        {
            var result = await _roleManager.DeleteAsync(entity);
            return result.Succeeded;
        }
    }
}