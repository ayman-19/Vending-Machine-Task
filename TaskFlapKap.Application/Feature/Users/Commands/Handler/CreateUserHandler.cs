using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Commands.Handler
{
	internal class CreateUserHandler : IRequestHandler<CreateUserRequest, UserResponse>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<CreateUserHandler> logger;

		public CreateUserHandler(IAccountService dbContext, ILogger<CreateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<UserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var response = await _dbContext.RegisterAsync(request.dto);
				logger.LogInformation($"User Updated");

				return response;
			}
			catch (Exception ex)
			{
				logger.LogError($"{ex.Message}");
				return new UserResponse
				{
					Massage = ex.Message,
				};
			}
		}
	}
}
