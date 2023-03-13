using AutoMapper;
using Shine.Backend.Core.Entities;

namespace Shine.Backend.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandProfile : Profile
    {
        public CreateUserCommandProfile()
        {
            CreateMap<CreateUserCommand, User>()
                .ForPath(entity => entity.FullName.First, options => options.MapFrom(dto => dto.FirstName))
                .ForPath(entity => entity.FullName.Last, options => options.MapFrom(dto => dto.LastName))
                .ForMember(entity => entity.NickName, options => options.MapFrom(dto => dto.NickName));
        }
    }
}