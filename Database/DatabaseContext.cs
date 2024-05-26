using Microsoft.EntityFrameworkCore;

namespace asp_net_restful_api_jwt.Database
{

    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }

}
