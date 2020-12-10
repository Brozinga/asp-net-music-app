﻿using System.Collections.Generic;
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

        public async Task<Music> Add(Music entity)
        {
           var result = Db.Musics.Add(entity);
           return result.Entity;
        }

        public async Task<Music> Update(Music entity)
        {
            var result = Db.Musics.Update(entity);
            return result.Entity;
        }

        public void Delete(Music entity)
        {
            Db.Musics.Remove(entity);
        }

        public async Task<IList<Music>> GetAllWhereUser(string userId)
        {
            var result = await Db.Musics
                .Include(x => x.MusicsToUsers)
                .ThenInclude(x => x.User).ToListAsync();

            return result;
        }

        public void AddRange(IList<Music> musics)
        {
            Db.Musics.AddRange(musics);
        }
    }
}