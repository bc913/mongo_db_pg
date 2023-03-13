using MediatR;
using System.Collections.Generic;

namespace Shine.Backend.Application.Features.Users.Queries.GetUsers
{
    public class GetUsersQuery : IRequest<List<UserLiteDto>>
    {
        
    }
}