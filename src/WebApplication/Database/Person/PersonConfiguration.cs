using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication.Database
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder
                .ToTable(nameof(Person));
            
            builder.
                Property(person => person.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder
                .Property(person => person.FirstName)
                .HasMaxLength(200)
                .IsRequired();
            
            builder
                .Property(person => person.LastName)
                .HasMaxLength(200)
                .IsRequired();
            
            builder
                .Property(person => person.MiddleName)
                .HasMaxLength(200);

            builder
                .HasMany(person => person.Books)
                .WithMany(book => book.Persons);
            
            builder
                .HasIndex(person => person.Id)
                .IsUnique();
        }
    }
}