using eventz.Data.Map;
using eventz.Models;
using Microsoft.EntityFrameworkCore;

namespace eventz.Data
{
    public class EventzDbContext : DbContext
    {
        public EventzDbContext(DbContextOptions<EventzDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<PersonModel> Person { get; set; }
        public DbSet<UserToken> Token { get; set; }
        //public DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
