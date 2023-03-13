using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shine.Backend.Core.Entities;
using Shine.Backend.Application.Contracts.Repositories;

namespace Shine.Backend.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IRepository<User> _repository;

        public DeleteUserCommandHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}