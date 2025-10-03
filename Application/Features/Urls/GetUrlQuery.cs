using Application.Common.Interfaces;
using Application.Features.Urls.Dto;
using Domain.Entity;
using MediatR;

namespace Application.Features.Urls
{
    public record GetUrlQuery() : IRequest<IEnumerable<UrlDto>>;
   

    public class GetUrlQueryHandler(IUrlService urlService) : IRequestHandler <GetUrlQuery,IEnumerable<UrlDto>>
    {
        private readonly IUrlService _urlService = urlService;
        public async Task<IEnumerable<UrlDto>> Handle(GetUrlQuery request, CancellationToken cancellationToken)
        {
           var result = await _urlService.GetUrlAsync();
           return result;
        }
    }
}
