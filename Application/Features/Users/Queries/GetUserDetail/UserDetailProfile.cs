using AutoMapper;
using Shine.Backend.Core.Entities;

namespace Shine.Backend.Application.Features.Users.Queries.GetUserDetail
{
    public class UserDetailProfile : Profile
    {
        public UserDetailProfile()
        {
            CreateMap<User, UserDetailDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.FirstName, options => options.MapFrom(src => src.FullName.First))
                .ForMember(dto => dto.LastName, options => options.MapFrom(src => src.FullName.Last))
                .ForMember(dto => dto.NickName, options => options.MapFrom(src => src.NickName));
        }
    }
}