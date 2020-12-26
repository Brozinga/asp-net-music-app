using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;

namespace MusicApp.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginRepository(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> Login(User user, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            return result;
        }

        public async Task<User> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }
    }
}