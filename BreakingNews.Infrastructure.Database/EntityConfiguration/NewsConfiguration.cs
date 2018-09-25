using BreakingNews.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BreakingNews.Infrastructure.Database.EntityConfiguration
{
    public class NewsConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");

            builder.HasKey(pk => pk.Id);

            builder.Property(p => p.FriendlyUrl)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.Content)
                .IsRequired();

            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.Property(p => p.Author)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(p => p.IsPublished)
                .IsRequired();

            builder.Property(p => p.PublishDate);

            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.Property(p => p.LastUpdateDate);

            builder.HasIndex(news => news.FriendlyUrl)
                .IsUnique();
        }
    }
}
