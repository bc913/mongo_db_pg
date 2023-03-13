using Bcan.Backend.SharedKernel.Contracts;

namespace Shine.Backend.Common.Interfaces
{
    public interface IAggregateRootWithId<IdType> : IAggregateRoot
    {
        IdType Id { get; }
    }
}