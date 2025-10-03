using System.Security.Cryptography;
using System.Text;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Helper
{
    public class UrlShortener(IOptions<TokenOptions> options)
    {
        private readonly IOptions<TokenOptions> _options = options;
        public string CreateShortUrl(string originalUrl)
        {
            string chars = _options.Value.Chars;
            var length = chars.Length / 5;
            var randomBytes = new byte[chars.Length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            var result = new StringBuilder(length);

            foreach (var b in randomBytes)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }
    }
}
