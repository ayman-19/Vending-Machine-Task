using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.Helper;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Handler
{
	public class DepositHandler : IRequestHandler<DepositRequest, string>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;
		private readonly ILogger<DepositHandler> logger;

		public DepositHandler(IUnitOfWork dbContext, IHttpContextAccessor httpContext, ILogger<DepositHandler> logger)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			this.logger = logger;
		}

		public async Task<string> Handle(DepositRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;

				var deposit = request.deposit;
				var user = await dbContext.Users.GetAsync(u => u.Id == userId, astracking: true);
				var amountafterchange = ChangeCoins.CalcCoins(deposit);
				user.Deposit += amountafterchange;
				await dbContext.SaveChangesAsync();
				await dbContext.CommitAsync();
				logger.LogInformation($"Deposit {user.UserName}");
				return "Deposited";
			}
			catch (Exception ex)
			{
				await dbContext.RollbackAsync();
				logger.LogError("Not Deposit!  " + ex.Message);
				return "Not Deposit!";
			}
		}
	}
}
