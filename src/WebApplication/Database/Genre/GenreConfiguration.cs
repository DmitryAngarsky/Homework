using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication.Database
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder
                .ToTable(nameof(Genre));
            
            builder
                .Property(genre => genre.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder
                .Property(genre => genre.GenreName)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .HasMany(genre => genre.Books)
                .WithMany(book => book.Genres);

            builder
                .HasIndex(genre => genre.Id)
                .IsUnique();
        }
    }
}