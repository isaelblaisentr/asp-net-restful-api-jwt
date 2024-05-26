using System.Text.Json.Serialization;

namespace asp_net_restful_api_jwt.Authentication
{
    public class UserLogin
    {
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class UserRegister
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateOnly? BirthDate { get; set; }
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string Role { get; set; } = "Everyone";
    }

    public class UserIdentity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Token { get; set; }
    }
}
