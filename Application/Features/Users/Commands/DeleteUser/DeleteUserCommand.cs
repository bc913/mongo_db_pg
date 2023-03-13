using MediatR;

namespace Shine.Backend.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; set; }
    }
}