namespace asp_net_restful_api_jwt.Database
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "Everyone";

        public User(string firstName, string lastName, string email, string password, string role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}
