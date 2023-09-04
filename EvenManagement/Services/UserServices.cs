using EvenManagement.Data;
using EvenManagement.Entities;
using EvenManagement.Requests.EventRequests;
using EvenManagement.Requests.UserRequests;
using EvenManagement.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace EvenManagement.Services
{
    public class UserServices : IUserServices

    {
        //DependencyInjection DbContext
        private AppDbContext _context;
        public UserServices(AppDbContext _appDbContext) {

            _context = _appDbContext;

        }
        public async Task<string> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return "User Created Successfully";
        }

        public async Task<string> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return "User Deleted Successfully";
        }

        public async Task<ICollection<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserAsync(Guid Userid)
        {
            return await _context.Users.Where(u => u.UserId == Userid).FirstOrDefaultAsync();

        }

        public async Task<User> GetUserbyEmailasync(string email)
        {
            return await _context.Users.Where(x => x.UserEmail == email).FirstOrDefaultAsync();
        }

        public Task<string> Loginasync(LoginUser loginrequest)
        {
            throw new NotImplementedException();
        }

        

        public async Task<string> RegisterEventAsync(EventRegister eventRegister)
        {
            // Check if the user and event exist
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserId == eventRegister.UserId);
            var events = await _context.Events.SingleOrDefaultAsync(x => x.Id == eventRegister.EventId);

            if (user == null || events == null)
            {
                return "User or event not found";
            }

            // Check if the user is already registered for the event
            if (user.Events.Contains(events))
            {
                return "User is already registered for this event";
            }

            // Add the event to the user's list of registered events
            user.Events.Add(events);

            try
            {
                await _context.SaveChangesAsync();
                return "User registered for the event successfully";
            }
            catch (Exception ex)
            {
                return $"An error occurred while registering the user for the event: {ex.Message}";
            }
        }


        public async Task<string> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return "User Updated Succesfully";
        }


    }
}
