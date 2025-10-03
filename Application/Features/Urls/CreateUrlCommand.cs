using Application.Common.Interfaces;
using Application.Features.Urls.Dto;
using Domain.Entity;
using Mapster;
using MediatR;

namespace Application.Features.Urls
{
    public record CreateUrlCommand(string OriginalUrl) : IRequest<ShortUrl>;

    public class CreateUrlCommandHandler(IUrlService urlService) : IRequestHandler<CreateUrlCommand, ShortUrl>
    {
        private readonly IUrlService _urlService = urlService;

        public async Task<ShortUrl> Handle(CreateUrlCommand request, CancellationToken cancellationToken)
        {
            var createUrlDto = request.Adapt<CreateUrlDto>();
            return await _urlService.CreateUrlAsync(createUrlDto);
        }
    }
}
