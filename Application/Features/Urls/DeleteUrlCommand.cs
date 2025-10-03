using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Urls
{
    public record DeleteUrlCommand(Guid Id) : IRequest;
  

    public class DeleteUrlCommandHandler(IUrlService urlService) : IRequestHandler<DeleteUrlCommand>
    {
        private readonly IUrlService _urlService = urlService;

        public async Task Handle(DeleteUrlCommand request, CancellationToken cancellationToken)
        {
            await _urlService.DeleteUrlAsync(request.Id);
        }
    }
}
