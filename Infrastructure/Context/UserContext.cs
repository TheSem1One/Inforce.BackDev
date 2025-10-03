using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Application.Features.UserContext;

namespace Infrastructure.Context
{
    public class UserContext : IUserContext
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;

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
