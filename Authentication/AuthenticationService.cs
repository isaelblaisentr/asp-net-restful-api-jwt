using asp_net_restful_api_jwt.Database;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace asp_net_restful_api_jwt.Authentication
{
    using BCrypt.Net;
    using Microsoft.EntityFrameworkCore;

    public class AuthenticationService : IAuthenticationService
    {

        private readonly DatabaseContext _databaseContext;
        private readonly IConfiguration _configuration;
        public AuthenticationService(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _databaseContext = databaseContext;
            _configuration = configuration;
        }

        public async Task<UserIdentity> Login(string email, string password)
        {
            User? user = await _databaseContext.Users.Where(user => user.Email == email)
                .FirstOrDefaultAsync();

            if (user == null || BCrypt.Verify(password, user.Password) == false)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["ApplicationSettings:JWT:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                }),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration["ApplicationSettings:JWT:Issuer"],
                Audience = _configuration["ApplicationSettings:JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var userToken = tokenHandler.CreateToken(tokenDescriptor);

            return new UserIdentity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.FirstName,
                Role = user.Role,
                Token = tokenHandler.WriteToken(userToken)
            };
        }

        public async Task<UserIdentity> Register(User user)
        {
            user.Password = BCrypt.HashPassword(user.Password);
            _databaseContext.Users.Add(user);
            await _databaseContext.SaveChangesAsync();

            return new UserIdentity()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.FirstName,
                Role = user.Role,
            };
        }
    }
}
