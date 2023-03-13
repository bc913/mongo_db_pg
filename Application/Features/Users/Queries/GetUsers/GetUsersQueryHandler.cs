using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Shine.Backend.Core.Entities;
using Shine.Backend.Application.Contracts.Repositories;

namespace Shine.Backend.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserLiteDto>>
    {
        private readonly IReadRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IMapper mapper, IReadRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<List<UserLiteDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await _userRepository.ListAsync(cancellationToken);
            return _mapper.Map<List<UserLiteDto>>(allUsers);
        }
    }
}