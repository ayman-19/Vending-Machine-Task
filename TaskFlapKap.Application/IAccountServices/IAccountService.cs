using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.IAccountServices
{
	public interface IAccountService
	{
		Task<UserResponse> LoginAsync(Login dto);
		Task<UserResponse> RegisterAsync(Register dto);
		Task<UserResponse> UpdateAsync(string id, UpdateUserdto dto);
	}
}
