using API.Context;
using Application.Common.Interfaces;
using Application.Features.Urls.Dto;
using Domain.Entity;
using Infrastructure.Helper;
using Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UrlServiceService(DatabaseContext db, UrlShortener shortener, UserContext context) : IUrlService
    {
        private readonly UserContext _context = context;
        private readonly UrlShortener _shortener = shortener;
        private readonly DatabaseContext _db = db;

        public async Task<ShortUrl> CreateUrlAsync(CreateUrlDto urlDto)
        {
            var url = new ShortUrl()
            {
                OriginalUrl = urlDto.OriginalUrl,
                ShortedUrl = _shortener.CreateShortUrl(urlDto.OriginalUrl),
                CreateById = _context.Id,
            };

            await _db.ShortUrl.AddAsync(url);
            await _db.SaveChangesAsync();

            return url;
        }

        public async Task<ShortUrl> GetUrlByAsync(Guid id)
        {
            var url = await _db.ShortUrl.FirstOrDefaultAsync(url => url.Id == id);
            return url;
        }

        public async Task<IEnumerable<UrlDto>> GetUrlAsync()
        {
            var urls = await _db.ShortUrl.ToListAsync();
            var getUrlDto = urls.Adapt<List<UrlDto>>();

            return getUrlDto;
        }

        public async Task DeleteUrlAsync(Guid id)
        {
            var url = await _db.ShortUrl.FirstOrDefaultAsync(url => url.Id == id);

            _db.ShortUrl.Remove(url);
            await _db.SaveChangesAsync();
        }
    }
}
