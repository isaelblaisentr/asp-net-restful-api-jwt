namespace asp_net_restful_api_jwt.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public required string Password { get; set; }
        public DateOnly? BirthDate { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
