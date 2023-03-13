using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shine.Backend.Core.Entities;
using Shine.Backend.Application.Contracts.Repositories;

namespace Shine.Backend.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
    {
        private readonly IRepository<User> _repository;

        private readonly IMapper _mapper;

        public UpdateUserCommandHandler(IMapper mapper, IRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            // Do validation

            var entity = _mapper.Map<User>(request);
            await _repository.UpdateAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}