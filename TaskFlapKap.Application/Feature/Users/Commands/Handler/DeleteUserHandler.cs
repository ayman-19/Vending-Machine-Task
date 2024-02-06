using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Users.Commands.Handler
{
	internal class DeleteUserHandler : IRequestHandler<DeleteUserRequest, string>
	{
		private readonly IUnitOfWork _dbContext;
		private readonly ILogger<UpdateUserHandler> logger;

		public DeleteUserHandler(IUnitOfWork dbContext, ILogger<UpdateUserHandler> logger)
		{
			_dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var user = await _dbContext.Users.GetAsync(u => u.Id.Equals(request.id));
				await _dbContext.Users.Delete(user);
				await _dbContext.SaveChangesAsync();
				await _dbContext.CommitAsync();
				logger.LogInformation($"User Deleted");
				return "Deleted";
			}
			catch (Exception ex)
			{
				logger.LogError($"{ex.Message}");
				return "Not Deleted";
			}
		}
	}
}
