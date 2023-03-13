using System.Threading;
using System.Threading.Tasks;
using Bcan.Backend.SharedKernel.Contracts;

namespace Shine.Backend.Application.Contracts.Repositories
{
    public interface IRepositoryBase<T, IdType> : IReadRepositoryBase<T, IdType> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(IdType id, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }

    public interface IRepository<T> : IRepositoryBase<T, string>, IReadRepository<T> where T : class, IAggregateRoot
    {        
    }
}