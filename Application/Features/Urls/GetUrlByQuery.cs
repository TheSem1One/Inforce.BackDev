using Application.Common.Interfaces;
using Domain.Entity;
using MediatR;

namespace Application.Features.Urls
{
    public record GetUrlByQuery(Guid Id) : IRequest<ShortUrl>;
   

    public class GetUrlByQueryHandler(IUrlService urlService) : IRequestHandler<GetUrlByQuery, ShortUrl>
    {
        private readonly IUrlService _urlService = urlService;
        public async Task<ShortUrl> Handle(GetUrlByQuery request, CancellationToken cancellationToken)
        {
            return await _urlService.GetUrlByAsync(request.Id);
        }
    }
}
