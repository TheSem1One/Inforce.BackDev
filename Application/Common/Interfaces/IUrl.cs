using Application.DTO.Url;
using Domain.Entity;

namespace Application.Common.Interfaces
{
    public interface IUrl
    {
        Task<bool> Create(CreateDto dto);
        Task<ShortUrl> Get(int id);
        Task<IEnumerable<GetUrlDto>> GetAll();
        Task<bool> Delete(int id);
    }
}
