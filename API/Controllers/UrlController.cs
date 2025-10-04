using Application.Features.Urls;
using Domain.Entity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UrlController(IMediator mediator) : ApiController
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<ActionResult<bool>> Create([FromBody] CreateUrlCommand urlCommand)
        {
            var result = await _mediator.Send(urlCommand);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<ShortUrl>>> GetAll()
        {
            var result = await _mediator.Send(new GetUrlQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrl>> Get(Guid id)
        {
            var result = await _mediator.Send(new GetUrlByQuery(id));
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ShortUrl>> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUrlCommand(id));
            return Ok();
        }

    }
}
