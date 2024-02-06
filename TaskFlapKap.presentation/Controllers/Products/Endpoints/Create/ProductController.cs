using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.presentation.Controllers.Products.Endpoints.Create
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
		[HttpPost]
		[Authorize(Roles = "Seller")]
		public async Task<IActionResult> CreateProduct(Productdto product)
		{
			var response = await mediator.Send(new CreateProductRequest(product));
			return Ok(response);
		}
	}
}
