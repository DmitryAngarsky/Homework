using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .ToTable(nameof(Book));
            
            builder
                .Property(book => book.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
            builder
                .Property(book => book.Name)
                .HasMaxLength(200)
                .IsRequired();
            
            builder
                .Property(book => book.AuthorId)
                .IsRequired();

            builder
                .HasIndex(b => b.Id)
                .IsUnique();
        }
    }
}