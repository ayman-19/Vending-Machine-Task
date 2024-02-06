using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Products.Commands.Validation
{
	public class DeleteProductValidation : AbstractValidator<DeleteProductRequest>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;

		public DeleteProductValidation(IUnitOfWork dbContext, IHttpContextAccessor httpContext)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
			CustomValidation();
		}
		private void CustomValidation()
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;
			RuleFor(p => p)
				.MustAsync(async (s, CancellationToken) => await dbContext.Products.IsAnyExistAsync(p => p.Id == s.id && p.SellerId == userId)).WithMessage("Product Id Is Exist or User Not Create this Product");
		}
	}
}
