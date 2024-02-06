using MediatR;

namespace TaskFlapKap.Application.Feather.Products.Commands.Request
{
	public record class DeleteProductRequest(int id) : IRequest<string>;
}
