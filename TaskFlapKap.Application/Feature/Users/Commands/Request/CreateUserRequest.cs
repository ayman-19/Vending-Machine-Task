using MediatR;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Commands.Request
{
	public record class CreateUserRequest(Register dto) : IRequest<UserResponse>;
}
