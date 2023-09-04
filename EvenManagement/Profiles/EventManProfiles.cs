using EvenManagement.Entities;
using AutoMapper;
using EvenManagement.Requests.UserRequests;
using EvenManagement.Requests.EventRequests;
using EvenManagement.Responses.EventResponse;
using EvenManagement.Responses.UserResponse;

namespace EvenManagement.Profiles
{
    public class EventManProfiles : Profile
    {
        public EventManProfiles() {

            CreateMap<AddUser, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<AddEvent , Event>().ReverseMap();
            CreateMap<EventResponse, Event>().ReverseMap();
            CreateMap<EventRegister , User>().ReverseMap();
        }
        
    }
}
