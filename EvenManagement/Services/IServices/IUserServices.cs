using EvenManagement.Entities;
using EvenManagement.Requests.EventRequests;
using EvenManagement.Requests.UserRequests;

namespace EvenManagement.Services.IServices
{
    public interface IUserServices
    {
        Task<string> AddUserAsync(User user);

        Task<string> UpdateUserAsync(User user);

        Task<string> DeleteUserAsync(User user);

        Task<User> GetUserAsync(Guid Userid);

        Task<ICollection<User>> GetAllUsersAsync();
        //get user by email
        Task<User> GetUserbyEmailasync(string email);
        //login
        Task<string> Loginasync(LoginUser loginrequest);
        
 
        Task<string> RegisterEventAsync(EventRegister eventRegister);
        
    }
}
