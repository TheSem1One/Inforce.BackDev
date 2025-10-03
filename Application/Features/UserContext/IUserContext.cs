namespace Application.Features.UserContext
{
    public interface IUserContext
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
