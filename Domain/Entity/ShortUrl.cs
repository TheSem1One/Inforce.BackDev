namespace Domain.Entity
{
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortedUrl { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }


    }
}
