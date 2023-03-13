using MediatR;

namespace Shine.Backend.Application.Features.Users.Queries.GetUserDetail
{
    public class GetUserDetailQuery : IRequest<UserDetailDto>
    {
        public string Id { get; set; }
    }
}