using System.Collections.Generic;
using System.Threading.Tasks;
using MusicApp.Domain.Models;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface IMusicRepository : IBaseRepository<Music>
    {
        Task<IList<Music>> GetAllWhereUser(string userId);
        void AddRange(IList<Music> musics);
    }
}