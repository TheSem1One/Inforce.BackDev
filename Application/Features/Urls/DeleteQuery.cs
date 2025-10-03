using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Urls
{
    public class DeleteQuery : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteQueryHandler(IUrl url) : IRequestHandler<DeleteQuery, bool>
    {
        private readonly IUrl _url = url;
        public async Task<bool> Handle(DeleteQuery request, CancellationToken cancellationToken)
        {
            return await _url.Delete(request.Id);
        }
    }
}
