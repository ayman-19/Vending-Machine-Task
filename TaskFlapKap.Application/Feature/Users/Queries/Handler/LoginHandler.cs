using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Handler;
using TaskFlapKap.Application.Feather.Users.Queries.Request;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.DataTransfareObject.Servicedto;

namespace TaskFlapKap.Application.Feather.Users.Queries.Handler
{
	internal class LoginHandler : IRequestHandler<LoginRequest, UserResponse>
	{
		private readonly IAccountService _dbContext;
		private readonly ILogger<CreateUserHandler> logger;

		public LoginHandler(IAccountService dbContext, ILogger<CreateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<UserResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _dbContext.LoginAsync(request.dto);
				logger.LogInformation($"{user.UserName} Retrived!");
				return user;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return new UserResponse { Massage = ex.Message };
			}
		}
	}
}
