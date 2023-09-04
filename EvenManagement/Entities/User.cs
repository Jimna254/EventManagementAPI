namespace EvenManagement.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public int PhoneNumber  { get; set; }
        public string Password { get; set; } = "";

        public string Role { get; set; } = "User"; //Default user

        public List<Event> Events { get; set; } = new List<Event>();


    }
}
