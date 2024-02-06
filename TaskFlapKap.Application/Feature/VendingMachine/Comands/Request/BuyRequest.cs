using MediatR;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Request
{
	public record class BuyRequest(List<BuyProductdto> dtos) : IRequest<ResponseBuyProduct>;
}
