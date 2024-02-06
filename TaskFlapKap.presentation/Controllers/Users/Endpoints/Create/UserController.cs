using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints.Create
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
		[HttpPost]
		public async Task<IActionResult> Register(Register register)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);
			var response = await mediator.Send(new CreateUserRequest(register));
			return Ok(response);
		}
	}
}
