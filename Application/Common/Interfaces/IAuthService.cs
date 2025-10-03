using Application.Features.Auth.Dto;

namespace Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);

        Task<string> RegisterAsync(RegisterDto dto);
    }
}
