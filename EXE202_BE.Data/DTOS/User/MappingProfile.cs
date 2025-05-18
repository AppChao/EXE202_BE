using AutoMapper;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Data.DTOS.User;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfiles, UserProfileResponse>()
            .ForMember(dest => dest.UPId, opt => opt.MapFrom(src => src.UPId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
    }
}