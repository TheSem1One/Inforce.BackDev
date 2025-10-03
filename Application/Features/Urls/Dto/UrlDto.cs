namespace Application.Features.Urls.Dto
{
    public class UrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortedUrl { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
    }
}
