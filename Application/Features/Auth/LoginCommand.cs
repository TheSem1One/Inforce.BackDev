using Application.Common.Interfaces;
using Application.Features.Auth.Dto;
using Mapster;
using MediatR;

namespace Application.Features.Auth
{
    public record LoginCommand(string Email, string Password) : IRequest<string>;

    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuthService _authService = authService;

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = request.Adapt<LoginDto>();
            return await _authService.LoginAsync(loginDto);
        }
    }
}
