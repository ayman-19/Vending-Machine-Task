using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Validation
{
	public class BuyValidation : AbstractValidator<BuyRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;

		public BuyValidation(IUnitOfWork dbContext, IHttpContextAccessor httpContext)
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

			RuleFor(u => u.dtos)
				.NotEmpty().WithMessage("List Not Empty")
				.NotNull().WithMessage("List Id Not Null")
				.MustAsync(async (p, CancellationToken)
				=> await Task.FromResult(p.Any(s => dbContext.Products.InValidProduct(inv => inv.Id == s.Id && inv.AmountAvailable >= s.Amount)))).WithMessage("Check Product and Amount Avilable")
				.Must(p => !p.Any(s => s.Amount < 0)).WithMessage("Not Enter Negative Value");
		}
	}
}
