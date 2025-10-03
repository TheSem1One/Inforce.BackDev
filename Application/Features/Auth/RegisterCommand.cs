using Application.Common.Interfaces;
using Application.DTO.Auth;
using Mapster;
using MediatR;

namespace Application.Features.Auth
{
    public class RegisterCommand : IRequest<string>
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class RegisterCommandHandler(IAuth auth) : IRequestHandler<RegisterCommand, string>
    {
        private readonly IAuth _auth = auth;
        public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var map = request.Adapt<RegisterDto>();
            return await _auth.Register(map);
        }
    }
}
