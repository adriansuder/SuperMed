using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SuperMed.DAL.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task CreateAsync(T item, CancellationToken cancellationToken);
        Task<T> GetAsync(int id, CancellationToken cancellationToken);
        Task<T> GetAsync(string name, CancellationToken cancellationToken);
        Task DeleteAsync(T item, CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<List<T>> ListAsync(CancellationToken cancellationToken);
        Task<T> Update(T item, CancellationToken cancellationToken);
    }
}
