using asp_net_restful_api_jwt.Database;

namespace asp_net_restful_api_jwt.Authentication
{
    public interface IAuthenticationService
    {
        public Task<UserIdentity> Login(string email, string password);
        public Task<UserIdentity> Register(User user);
    }
}
