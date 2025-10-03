using Application.DTO.Auth;

namespace Application.Common.Interfaces
{
    public interface IAuth
    {
        Task<string> Login(LoginDto dto);
        Task<string> Register(RegisterDto dto);
    }
}
