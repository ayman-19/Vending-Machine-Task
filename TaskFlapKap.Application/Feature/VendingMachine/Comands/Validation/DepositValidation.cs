using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Validation
{
	public class DepositValidation : AbstractValidator<DepositRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;

		public DepositValidation(IUnitOfWork dbContext, IHttpContextAccessor httpContext)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			CustomValidation();
		}
		private void CustomValidation()
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;

			RuleFor(u => userId)
				.NotEmpty().WithMessage("User Id Not Empty")
				.NotNull().WithMessage("User Id Not Null")
				.MustAsync(async (s, CancellationToken) => await dbContext.Users.IsAnyExistAsync(u => u.Id == s)).WithMessage("User Not Exist");

			RuleFor(u => u.deposit)
				.NotEmpty().WithMessage("Deposit Not Empty")
				.NotNull().WithMessage("Deposit Not Null")
				.GreaterThan(0).WithMessage("Not Enter Negative Value!");
		}
	}
}
