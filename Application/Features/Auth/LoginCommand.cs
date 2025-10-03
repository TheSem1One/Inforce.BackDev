using Application.Common.Interfaces;
using Application.DTO.Auth;
using Mapster;
using MediatR;

namespace Application.Features.Auth
{
    public class LoginCommand : IRequest<string>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginCommandHandler(IAuth auth) : IRequestHandler<LoginCommand, string>
    {
        private readonly IAuth _auth = auth;
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
           var map = request.Adapt<LoginDto>();
           return await _auth.Login(map);
        }
    }
}
