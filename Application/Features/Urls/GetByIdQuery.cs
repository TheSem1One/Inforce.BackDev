using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;

namespace Application.Features.Urls
{
    public class GetByIdQuery : IRequest<ShortUrl>
    {
        public int Id { get; set; }
    }

    public class GetByIdHandler(IUrl url): IRequestHandler<GetByIdQuery, ShortUrl>
    {
        private IUrl _url = url;
        public async Task<ShortUrl> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _url.Get(request.Id);
            return result;
        }
    }
}
