using System.Reflection;
using Domain;
using Microsoft.EntityFrameworkCore;
using WebApplication.Database;

namespace Database
{
    public sealed class Context : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> Persons { get; set; }
        
        public Context(DbContextOptions options) : base(options)
        {
        
        // Database.EnsureDeleted();
        // Database.EnsureCreatedAsync();
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()).Seed();
        }
    }
}