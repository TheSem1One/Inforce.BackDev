using Domain.Entity;
using Domain.Enums;

namespace Domain.Entities
{

    public class User
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public UserRole Role { get; set; } = UserRole.User;


        public ICollection<ShortUrl> ShortUrls { get; set; } = new List<ShortUrl>();
    }

}
