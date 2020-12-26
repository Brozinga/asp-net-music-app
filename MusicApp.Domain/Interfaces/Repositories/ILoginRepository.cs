using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MusicApp.Domain.Models;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<SignInResult> Login(User user, string password);
        Task<User> FindUserByEmail(string email);
    }
}