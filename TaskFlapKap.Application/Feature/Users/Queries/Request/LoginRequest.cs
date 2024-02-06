using MediatR;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Queries.Request
{
	public record class LoginRequest(Login dto) : IRequest<UserResponse>;
}
