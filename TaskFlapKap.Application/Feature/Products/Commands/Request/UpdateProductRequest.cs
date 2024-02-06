using MediatR;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Commands.Request
{
	public record class UpdateProductRequest(int id, Productdto Product) : IRequest<string>;
}
