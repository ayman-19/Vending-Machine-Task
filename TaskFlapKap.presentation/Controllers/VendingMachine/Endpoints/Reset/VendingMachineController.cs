using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;

namespace TaskFlapKap.presentation.Controllers.VendingMachine.Endpoints.Reset
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public partial class VendingMachineController : ControllerBase
	{
		private readonly IMediator mediator;

		public VendingMachineController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet]
		[Authorize(Roles = "Buyer")]
		public async Task<IActionResult> Rest()
		{
			return Ok(await mediator.Send(new ResetRequest()));
		}
	}
}
