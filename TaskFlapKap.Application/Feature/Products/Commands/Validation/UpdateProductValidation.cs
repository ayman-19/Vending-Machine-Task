using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Products.Commands.Validation
{
	public class UpdateProductValidation : AbstractValidator<UpdateProductRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;

		public UpdateProductValidation(IUnitOfWork dbContext, IHttpContextAccessor httpContext)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			CustomValidation();
		}
		private void CustomValidation()
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;

			RuleFor(p => p)
				 .MustAsync(async (s, CancellationToken) => await dbContext.Products.IsAnyExistAsync(p => p.Id == s.id && p.SellerId == userId)).WithMessage("Product Id Is not Exist or User Not Create this Product");

			RuleFor(p => p.Product.ProductName)
				.NotNull().WithMessage("Name Is Not Null")
				.NotEmpty().WithMessage("Name Is Not Empty");

			RuleFor(p => p)
				.MustAsync(async (s, CancellationToken) => !await dbContext.Products.IsAnyExistAsync(p => p.ProductName.Equals(s.Product.ProductName) && p.Id != s.id)).WithMessage("Name Is Exist");

			RuleFor(p => p.Product.AmountAvailable)
			.NotNull().WithMessage("AmountAvailable Is Not Null")
			.NotEmpty().WithMessage("AmountAvailable Is Not Empty");

			RuleFor(p => p.Product.Cost)
			.NotNull().WithMessage("Cost Is Not Null")
			.NotEmpty().WithMessage("Cost Is Not Empty");


			RuleFor(p => userId)
				.MustAsync(async (s, CancellationToken) => await dbContext.Users.IsAnyExistAsync(p => p.Id.Equals(s))).WithMessage("Seller Id Is Not Exist");
			RuleFor(p => p.Product.AmountAvailable).GreaterThan(0).WithMessage("Not Enter Negative value!");
		}

	}
}
