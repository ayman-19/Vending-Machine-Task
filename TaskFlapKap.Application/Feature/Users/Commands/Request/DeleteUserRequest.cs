using MediatR;

namespace TaskFlapKap.Application.Feather.Users.Commands.Request
{
	public record class DeleteUserRequest(string id) : IRequest<string>;
}
