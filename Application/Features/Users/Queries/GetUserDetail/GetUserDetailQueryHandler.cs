using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Shine.Backend.Core.Entities;
using Shine.Backend.Application.Exceptions;
using Shine.Backend.Application.Contracts.Repositories;

namespace Shine.Backend.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailDto>
    {
        private readonly IReadRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IMapper mapper, IReadRepository<User> userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository.GetByIdAsync(request.Id);
            if(entity is null)
                throw new NotFoundException(nameof(User), request.Id);

            var dto = _mapper.Map<UserDetailDto>(entity);
            return dto;
        }
    } 
}
