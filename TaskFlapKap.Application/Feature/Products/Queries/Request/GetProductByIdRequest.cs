using MediatR;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Queries.Request
{
	public record class GetProductByIdRequest(int id) : IRequest<ProductResponse>;
}
