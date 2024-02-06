using MediatR;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Request
{
	public record class DepositRequest(double deposit) : IRequest<string>;
}
