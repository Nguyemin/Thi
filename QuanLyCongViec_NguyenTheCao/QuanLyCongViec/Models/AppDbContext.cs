using Microsoft.EntityFrameworkCore;

    namespace QuanLyCongViec.Models
    {
        public class AppDbContext : DbContext
        {
            public DbSet<Congviec> Congviec { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {

            }
        }
    }

