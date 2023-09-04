using EvenManagement.Data;
using EvenManagement.Entities;
using EvenManagement.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace EvenManagement.Services
{
    public class EventServices : IEventServices
    {
        //DependencyInjection DbContext
        private AppDbContext _context;
        public EventServices(AppDbContext _appDbContext)
        {

            _context = _appDbContext;

        }
        public async Task<string> AddEventAsync(Event _event)
        {
            _context.Events.Add(_event);
            await _context.SaveChangesAsync();
            return "Event Created Successfully";
        }

        

        public async Task<string> DeleteEventAsync(Event _event)
        {
            _context.Events.Remove(_event);
            await _context.SaveChangesAsync();
            return "Event Deleted Successfully";
        }

        public async Task<ICollection<Event>> GetAllEventsAsync(string? location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return await _context.Events.ToListAsync();

            }
            return await _context.Events.Where(e => e.Location == location).ToListAsync();
        }

       

        public async Task<Event> GetEventAsync(Guid Id)
        {
            return await _context.Events.Where(e => e.Id == Id).FirstOrDefaultAsync();

        }

        public async Task<string> UpdateEventAsync(Event _event)
        {
            _context.Events.Update(_event);
            await _context.SaveChangesAsync();
            return "Evented Updated Succesfully";

        }
        //Get Users that have registered for the event
        public async Task<List<User>> GetAllUsersRegisteredForAnEvent(Guid id)
        {
            var result = await _context.Events.Include(x => x.Registered_Users).FirstOrDefaultAsync(x => x.Id == id);
            return result.Registered_Users;
        }

        //Get the remaining slots
        public async Task<int> AvailableSlots(Guid id)
        {
            var result = await _context.Events.FirstOrDefaultAsync(x => x.Id == id);
            var availableSlots = result.Capacity - result.Registered_Users.Count;
            return availableSlots;
        }
    }
}
