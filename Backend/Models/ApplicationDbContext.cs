using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.Entities;

namespace Models
{
    /* Class that connects EntityFramework and SQL Server */
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TaskToDo> Task { get; set; }

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Connection");
                builder.UseSqlServer(connectionString);
            }
        }
    }
}
