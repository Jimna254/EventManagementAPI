using EvenManagement.Entities;

namespace EvenManagement.Services.IServices
{
    public interface IEventServices
    {
        Task<string> AddEventAsync(Event _event);

        Task<string> UpdateEventAsync(Event _event);

        Task<string> DeleteEventAsync(Event _event);

        Task<Event> GetEventAsync(Guid id);

        Task<ICollection<Event>> GetAllEventsAsync(string? location);


        //get all users who registered for an event
        Task<List<User>> GetAllUsersRegisteredForAnEvent(Guid id);
        //get available slots
        Task<int> AvailableSlots(Guid id);
    }
}
