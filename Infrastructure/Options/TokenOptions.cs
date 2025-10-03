namespace Infrastructure.Options
{
    public class TokenOptions
    {
        public const string SectionName = "Secret-Token";
        public string Token { get; set; } = null!;
        public string Chars { get; set; } = null!;
        public int Length { get; set; }
    }
}
