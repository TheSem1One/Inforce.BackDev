using Application.Common.Interfaces;
using Application.DTO.Url;
using Domain.Entity;
using Infrastructure.Helper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UrlService(DatabaseContext db, UrlShortener shortener) : IUrl
    {
        private readonly UrlShortener _shortener = shortener;
        private readonly DatabaseContext _db = db;
        public async Task<bool> Create(CreateDto dto)
        {
            await UrlExists(dto.OriginalUrl);

            var ulr = new ShortUrl()
            {
                OriginalUrl = dto.OriginalUrl,
                ShortedUrl = _shortener.CreateShortUrl(dto.ShortedUrl),
                CreateBy = dto.CreateBy
            };
            await _db.ShortUrl.AddAsync(ulr);
            var result = await _db.SaveChangesAsync();

            return result > 0 ? true : throw new Exception("Failed to Create Link");

        }

        public async Task<ShortUrl> Get(int id)
        {
            var url = await _db.ShortUrl.FirstOrDefaultAsync(url => url.Id == id);
            return url;
        }

        public async Task<IEnumerable<GetUrlDto>> GetAll()
        {

            var urls = await _db.ShortUrl.Select(u => true).ToListAsync();
            return (IEnumerable<GetUrlDto>)urls;
        }

        public async Task<bool> Delete(int id)
        {
            var url = await _db.ShortUrl.FirstOrDefaultAsync(url => url.Id == id);
            _db.ShortUrl.Remove(url);
            var result = await _db.SaveChangesAsync();
            return result > 0 ? true : throw new Exception("Failed to Delete Link");
        }

        public async Task UrlExists(string originalUrl)
        {
            if (await _db.ShortUrl.AnyAsync(url => url.OriginalUrl.ToLower().Equals(originalUrl.ToLower())))
            {
                throw new Exception("Url does not exist");
            }
        }
    }
}
