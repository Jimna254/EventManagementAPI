using System.ComponentModel.DataAnnotations;

namespace EvenManagement.Requests.UserRequests
{
    public class LoginUser
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        
        public string Password { get; set; } = "";
    }
}
