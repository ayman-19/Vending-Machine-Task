using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Queries.Request;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints.GetById
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
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductById(int id)
		{
			var response = await mediator.Send(new GetProductByIdRequest(id));
			return Ok(response);
		}
	}
}
