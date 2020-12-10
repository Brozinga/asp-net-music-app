using System.Threading.Tasks;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        void Delete(T entity);
    }
}