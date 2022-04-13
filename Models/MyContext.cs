using Microsoft.EntityFrameworkCore;

namespace ModelProject.Models {

    public class MyContext : DbContext {

        public MyContext (DbContextOptions options) : base (options) { }

        public DbSet<ClientUser> ClientUsers { get; set; }
        public DbSet<ModelUser> ModelUsers { get; set; }
        public DbSet<App> Apps { get; set; }

        
        // public DbSet<Association> Associations { get; set; }
    }
}
