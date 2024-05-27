using asp_net_restful_api_jwt.Database;
using Microsoft.AspNetCore.Mvc;

namespace asp_net_restful_api_jwt.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        public UserController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_databaseContext.Users == null)
            {
                return NotFound();
            }
            var user = await _databaseContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

    }
}
