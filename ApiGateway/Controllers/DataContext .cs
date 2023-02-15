namespace ApiGateway.Controllers
{
    using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Dump>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToTable("dump");
                    });

        }

        public DbSet<Dump> Dump { get; set; }
    }
}
