using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints.Delete
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public partial class UserController : ControllerBase
	{
		private readonly IMediator mediator;

		public UserController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpDelete("{id}")]
		[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			var response = await mediator.Send(new DeleteUserRequest(id));
			return Ok(response);
		}
	}
}
