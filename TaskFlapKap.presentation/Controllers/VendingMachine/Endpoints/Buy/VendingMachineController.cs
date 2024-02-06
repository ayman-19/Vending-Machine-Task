using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.presentation.Controllers.VendingMachine.Endpoints.Buy
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
		[HttpPost]
		[Authorize(Roles = "Buyer")]
		public async Task<IActionResult> Buy(List<BuyProductdto> buys)
		{
			return Ok(await mediator.Send(new BuyRequest(buys)));
		}
	}
}
