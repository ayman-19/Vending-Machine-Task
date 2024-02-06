using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;

namespace TaskFlapKap.presentation.Controllers.VendingMachine.Endpoints.Deposit
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

		[HttpGet("{deposit}")]
		[Authorize(Roles = "Buyer")]
		public async Task<IActionResult> Deposit(double deposit)
		{
			return Ok(await mediator.Send(new DepositRequest(deposit)));
		}
	}
}
