using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Events
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventDto>();
            CreateMap<EventCreationDto, Event>();
            CreateMap<EventUpdatingDto, Event>();

            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
