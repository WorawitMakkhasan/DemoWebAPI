using WebApiDemo.Model;

namespace WebApiDemo.Datasource
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<Location> Locations { get; set; }

    }
}
