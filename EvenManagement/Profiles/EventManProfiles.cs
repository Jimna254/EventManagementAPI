﻿using EvenManagement.Entities;
using EvenManagement.Requests;
using EvenManagement.Responses;
using AutoMapper;

namespace EvenManagement.Profiles
{
    public class EventManProfiles : Profile
    {
        public EventManProfiles() {

            CreateMap<AddUser, User>().ReverseMap();
            CreateMap<UserResponse, User>().ReverseMap();
            CreateMap<AddEvent , Event>().ReverseMap();
            CreateMap<EventResponse, Event>().ReverseMap();
        }
        
    }
}
