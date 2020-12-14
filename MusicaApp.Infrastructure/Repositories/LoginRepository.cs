using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;

namespace MusicApp.Infrastructure.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly SignInManager<User> _signInManager;

        public LoginRepository(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<SignInResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            return result;
        }
    }
}