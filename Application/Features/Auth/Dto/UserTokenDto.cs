using Domain.Enums;

namespace Application.Features.Auth.Dto
{
    public class UserTokenDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public UserRole Role { get; set; } 
    }
}
