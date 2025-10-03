using Application.Features.Urls.Dto;
using Domain.Entity;

namespace Application.Common.Interfaces
{
    public interface IUrlService
    {
        Task<ShortUrl> CreateUrlAsync(CreateUrlDto urlDto);
        Task<ShortUrl> GetUrlByAsync(Guid id);
        Task<IEnumerable<UrlDto>> GetUrlAsync();
        Task DeleteUrlAsync(Guid id);
    }
}
