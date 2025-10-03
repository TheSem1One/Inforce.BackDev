using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace API.Context
{
    public class UserContext
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public UserContext(IHttpContextAccessor httpContextAccessor, ILogger<UserContext> logger)
        {
            var httpContext = httpContextAccessor.HttpContext;
            if (httpContext is null)
            {
                logger.LogWarning("HttpContext is null");
                return;
            }

            var userClaims = httpContext.User;
            Id = Guid.Parse(userClaims.FindFirstValue("Id")!);
            Username = userClaims.FindFirstValue(ClaimTypes.Name)!;
            Role = userClaims.FindFirstValue(ClaimTypes.Role)!;
        }
    }
}
