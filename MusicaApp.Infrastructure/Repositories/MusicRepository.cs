using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicApp.Domain.Interfaces.Repositories;
using MusicApp.Domain.Models;
using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Infrastructure.Repositories
{
    public class MusicRepository : BaseRepository, IMusicRepository
    {
        public MusicRepository(SqliteContext db) : base(db)
        {
        }

        public Task<bool> Add(Music entity)
        {
           var result = Db.Musics.Add(entity);
           return Task.FromResult(result.State == EntityState.Added);
        }

        public Task<bool> Update(Music entity)
        {
            var result = Db.Update(entity);
            return Task.FromResult(result.State == EntityState.Modified);
        }

        public Task<bool> Delete(Music entity)
        {
            var result = Db.Musics.Remove(entity);
            return Task.FromResult(result.State == EntityState.Deleted);
        }

        public async Task<IList<Music>> GetAllWhereUser(string userId, int skip, int take)
        {
            var result = await Db.Musics
                .Skip(skip)
                .Take(take)
                .Include(x => x.MusicsToUsers)
                .ThenInclude(x => x.User)
                .ToListAsync();

            return result;
        }

        public void AddRange(IList<Music> musics)
        {
            Db.Musics.AddRange(musics);
        }
    }
}