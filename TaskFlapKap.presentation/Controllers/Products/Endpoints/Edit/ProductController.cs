using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints.Edit
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
		[HttpPut("{id}")]
		[Authorize(Roles = "Seller")]
		public async Task<IActionResult> Edit(int id, Productdto update)
		{
			var response = await mediator.Send(new UpdateProductRequest(id, update));
			return Ok(response);
		}
	}
}
