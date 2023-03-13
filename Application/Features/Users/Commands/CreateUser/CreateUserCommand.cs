using MediatR;
using System;

namespace Shine.Backend.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
    }

}