using AutoMapper;
using Shine.Backend.Core.Entities;

namespace Shine.Backend.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandProfile : Profile
    {
        public UpdateUserCommandProfile()
        {
            CreateMap<UpdateUserCommand, User>()
                .ForMember(entity => entity.Id, options => options.MapFrom(dto => dto.Id))
                .ForPath(entity => entity.FullName.First, options => options.MapFrom(dto => dto.FirstName))
                .ForPath(entity => entity.FullName.Last, options => options.MapFrom(dto => dto.LastName))
                .ForMember(entity => entity.NickName, options => options.MapFrom(dto => dto.NickName));
        }
    }
}