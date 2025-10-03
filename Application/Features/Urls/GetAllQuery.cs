using Application.Common.Interfaces;
using Application.DTO.Url;
using Domain.Entity;
using MediatR;

namespace Application.Features.Urls
{
    public class GetAllQuery : IRequest<IEnumerable<GetUrlDto>>
    {
    }

    public class GetAllQueryHandler(IUrl url) : IRequestHandler <GetAllQuery,IEnumerable<GetUrlDto>>
    {
        private readonly IUrl _url = url;
        public async Task<IEnumerable<GetUrlDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
           var result = await _url.GetAll();
           return result;
        }
    }
}
