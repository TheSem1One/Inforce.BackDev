namespace Application.Features.Urls.Dto
{
    public class UrlDto
    {
        public Guid Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortedUrl { get; set; } = null!;
        public Guid CreatedById { get; set; }
        public string CreatorUsername { get; set; }
    }
}
