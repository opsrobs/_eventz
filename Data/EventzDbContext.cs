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
        public DbSet<Event> Event { get; set; }
        public DbSet<Localization> Localization { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Home> Home { get; set; }
        //public DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new PersonMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.ApplyConfiguration(new EventMap());
            modelBuilder.ApplyConfiguration(new LocalizationMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new SectionMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
