using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface ILoginRepository
    {
        Task<SignInResult> Login(string email, string password);
    }
}