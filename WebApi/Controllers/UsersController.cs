
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Shine.Backend.Application.Features.Users.Queries.GetUsers;
using Shine.Backend.Application.Features.Users.Queries.GetUserDetail;
using Shine.Backend.Application.Features.Users.Commands.CreateUser;
using Shine.Backend.Application.Features.Users.Commands.UpdateUser;
using Shine.Backend.Application.Features.Users.Commands.DeleteUser;

namespace Shine.Backend.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetUserDetailQuery() { Id = id };
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtRoute("GetUser", new {id = id}, command);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete([FromRoute]string id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        // If exists update the resource (returns 200 or 204). 
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
