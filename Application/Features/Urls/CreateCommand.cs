
using Application.Common.Interfaces;
using Application.DTO.Url;
using Mapster;
using MediatR;

namespace Application.Features.Urls
{
    public class CreateCommand : IRequest<bool>
    {
        public string OriginalUrl { get; set; } = null!;
        public string ShortedUrl { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
    }

    public class CreateCommandHandler (IUrl url): IRequestHandler<CreateCommand,bool>
    {
        private readonly IUrl _url=url;
        public async Task<bool> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var map = request.Adapt<CreateDto>();
            return  await _url.Create(map);
        }
    }
}
