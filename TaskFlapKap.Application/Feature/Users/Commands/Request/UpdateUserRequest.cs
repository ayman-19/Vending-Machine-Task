using MediatR;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Commands.Request
{
	public record class UpdateUserRequest(string id, UpdateUserdto dto) : IRequest<UserResponse>;
}
