using Domain.Entities;

namespace Domain.Entity
{
    public class ShortUrl
    {
        public Guid Id { get; set; }

        public string OriginalUrl { get; set; } = null!;

        public string ShortedUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User CreateBy { get; set; }
        public Guid CreateById { get; set; }
    }
}
