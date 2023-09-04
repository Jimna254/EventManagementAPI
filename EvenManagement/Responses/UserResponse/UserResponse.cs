namespace EvenManagement.Responses.UserResponse
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public string Role { get; set; } = "User";
        public int PhoneNumber { get; set; }

    }
}
