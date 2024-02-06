using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Commands.Request;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints.Delete
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
		[HttpDelete("{id}")]
		[Authorize(Roles = "Seller")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var response = await mediator.Send(new DeleteProductRequest(id));
			return Ok(response);
		}
	}
}
