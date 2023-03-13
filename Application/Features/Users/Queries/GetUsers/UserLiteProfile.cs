using AutoMapper;
using Shine.Backend.Core.Entities;

namespace Shine.Backend.Application.Features.Users.Queries.GetUsers
{
    public class UserLiteProfile : Profile
    {
        public UserLiteProfile()
        {
            /*
            For valid Configuration
            Consolidate the CreateMap calls into one profile, or set the root Advanced.AllowAdditiveTypeMapCreation configuration value to 'true'.
            */
            CreateMap<User, UserLiteDto>()
                .ForMember(dto => dto.Id, options => options.MapFrom(src => src.Id))
                .ForMember(dto => dto.NickName, options => options.MapFrom(src => src.NickName))
                .ForMember(dto => dto.FullName, options => options.MapFrom(src => src.FullName.AsFormatted()));
        }
    }
}