using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ShortUrlConfiguration : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> builder)
        {
            builder.HasKey(shortUrl => shortUrl.Id);

            builder
                .HasIndex(shortUrl => shortUrl.OriginalUrl)
                .IsUnique();

            builder.HasOne(x => x.Creator)
                .WithMany(s => s.ShortUrls)
                .HasForeignKey(x => x.CreatorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
