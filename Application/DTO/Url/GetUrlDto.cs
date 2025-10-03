namespace Application.DTO.Url
{
    public class GetUrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; } = null!;
        public string ShortedUrl { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
    }
}
