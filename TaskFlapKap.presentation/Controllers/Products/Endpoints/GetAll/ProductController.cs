using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Queries.Request;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints.GetAll
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public partial class ProductController : ControllerBase
	{
		private readonly IMediator mediator;

		public ProductController(IMediator mediator)
		{
			this.mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllProduct()
		{
			var response = await mediator.Send(new GatAllProductRequest());
			return Ok(response);
		}
	}
}
