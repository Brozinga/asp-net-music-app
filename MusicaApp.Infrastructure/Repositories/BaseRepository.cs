using MusicApp.Infrastructure.Contexts;

namespace MusicApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SqliteContext Db;

        protected BaseRepository(SqliteContext db)
        {
            Db = db;
        }


    }
}