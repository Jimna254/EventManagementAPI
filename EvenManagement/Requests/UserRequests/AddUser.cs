using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EvenManagement.Requests.UserRequests
{
    public class AddUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public int PhoneNumber { get; set; }
        public string Password { get; set; } = string.Empty;
    }

}
