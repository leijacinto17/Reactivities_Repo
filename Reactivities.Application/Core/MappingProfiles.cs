using Application.DTOs.Accounts;
using Application.DTOs.Activities;
using AutoMapper;
using Domain.Models;

namespace Application.Core
{
    //TODO: Delete if not use anymore
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Activity, Activity>();
            CreateMap<Activity, ActivityDto>()
               .ForMember(d => d.HostUsername, o => o.MapFrom(s => s.Attendees
                   .FirstOrDefault(x => x.IsHost).User.UserName));
            CreateMap<ActivityAttendee, ProfileDto>()
                .ForMember(d => d.DisplayName, o => o.MapFrom(s => s.User.DisplayName))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.UserName))
                .ForMember(d => d.Bio, o => o.MapFrom(s => s.User.Bio));
        }
    }
}
