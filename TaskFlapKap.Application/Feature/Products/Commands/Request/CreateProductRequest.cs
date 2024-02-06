using MediatR;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Commands.Request
{
	public record class CreateProductRequest(Productdto Product) : IRequest<string>;
}
