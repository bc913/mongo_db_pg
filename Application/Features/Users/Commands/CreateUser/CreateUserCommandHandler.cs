using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shine.Backend.Application.Contracts.Repositories;
using Shine.Backend.Core.Entities;

namespace Shine.Backend.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly IRepository<User> _repository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IMapper mapper, IRepository<User> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Do validations

            var entity = _mapper.Map<User>(request);
            entity = await _repository.AddAsync(entity, cancellationToken);
            return entity.Id;

        }
    }
}