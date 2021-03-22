using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication.Database
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .ToTable(nameof(Author));

            builder.
                Property(author => author.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder
                .Property(author => author.FirstName)
                .HasMaxLength(200)
                .IsRequired();
            
            builder
                .Property(author => author.LastName)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property(author => author.MiddleName)
                .HasMaxLength(200);

            builder
                .HasMany(author => author.Books)
                .WithOne();
            
            builder
                .HasIndex(author => author.Id)
                .IsUnique();
        }
    }
}