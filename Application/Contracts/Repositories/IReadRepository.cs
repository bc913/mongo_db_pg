using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Bcan.Backend.SharedKernel.Contracts;

namespace Shine.Backend.Application.Contracts.Repositories
{

    public interface IReadRepositoryBase<T, IdType> where T : class
    {
        Task<List<T>> ListAsync(CancellationToken cancellationToken = default);
        Task<T> GetByIdAsync(IdType id, CancellationToken cancellationToken = default) /*where IdType : notnull*/;
    }
    public interface IReadRepository<T> : IReadRepositoryBase<T, string> where T : class, IAggregateRoot
    {
    }
}