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
        public async Task<ActionResult<bool>> Create([FromBody] CreateCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<ShortUrl>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShortUrl>> Get(int id)
        {
            var result = await _mediator.Send(new GetByIdQuery { Id = id });
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<ShortUrl>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteQuery { Id = id });
            return Ok(result);
        }

    }
}
