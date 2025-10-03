using Application.Common.Interfaces;
using Application.Features.Auth.Dto;
using Mapster;
using MediatR;

namespace Application.Features.Auth
{
    public record RegisterCommand(string Username, string Email,
        string Password, string ConfirmPassword) : IRequest<string>;


    public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuthService _authService = authService;

        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = request.Adapt<RegisterDto>();
            return await _authService.RegisterAsync(registerDto);
        }
    }
}
