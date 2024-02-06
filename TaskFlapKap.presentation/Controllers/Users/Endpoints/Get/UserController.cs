using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Users.Queries.Request;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.presentation.Controllers.Users.Endpoints.Get
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
		public async Task<IActionResult> Login(Login login)
		{
			var response = await mediator.Send(new LoginRequest(login));
			return Ok(response);
		}
	}
}
