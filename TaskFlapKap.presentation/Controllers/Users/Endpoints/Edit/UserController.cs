using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints.Edit
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
		[HttpPut("{id}")]
		[Authorize]
		public async Task<IActionResult> Edit(string id, UpdateUserdto user)
		{
			var response = await mediator.Send(new UpdateUserRequest(id, user));
			return Ok(response);
		}
	}
}
