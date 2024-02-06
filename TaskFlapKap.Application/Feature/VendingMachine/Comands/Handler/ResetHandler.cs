using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Handler
{
	public class ResetHandler : IRequestHandler<ResetRequest, string>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;
		private readonly ILogger<DepositHandler> logger;

		public ResetHandler(IUnitOfWork dbContext, IHttpContextAccessor httpContext, ILogger<DepositHandler> logger)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			this.logger = logger;
		}
		public async Task<string> Handle(ResetRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;

				var user = await dbContext.Users.GetAsync(u => u.Id == userId, astracking: true);
				user.Deposit = 0;
				await dbContext.SaveChangesAsync();
				await dbContext.CommitAsync();
				logger.LogInformation($"Reset {user.UserName}");
				return "Reset";
			}
			catch (Exception ex)
			{
				logger.LogError("Not Reset!" + ex.Message);
				await dbContext.RollbackAsync();
				return "Not Reset!";
			}
		}
	}
}
