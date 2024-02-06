using MediatR;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Queries.Request
{
	public record class GatAllProductRequest : IRequest<List<ProductResponse>>;
}
