using MediatR;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Request
{
	public record class ResetRequest() : IRequest<string>;
}
