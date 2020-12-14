using System.Threading.Tasks;

namespace MusicApp.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<in T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}