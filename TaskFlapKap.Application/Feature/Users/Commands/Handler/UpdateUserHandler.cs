using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Commands.Handler
{
	public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, UserResponse>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<UpdateUserHandler> logger;

		public UpdateUserHandler(IAccountService dbContext, ILogger<UpdateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<UserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _dbContext.UpdateAsync(request.id, request.dto);
				logger.LogInformation($"User Updated");
				return user;
			}
			catch (Exception ex)
			{
				logger.LogError($"{ex.Message}");
				return new UserResponse { Massage = ex.Message };
			}
		}
	}
}
