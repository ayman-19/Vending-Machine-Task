using FluentValidation;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Products.Queries.Validation
{
	public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
	{
		private readonly IUnitOfWork dbContext;

		public GetProductByIdValidator(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		private void CustomValidation()
		{
			RuleFor(p => p.id)
				.MustAsync(async (s, CancellationToken) => await dbContext.Products.IsAnyExistAsync(p => p.Id == s)).WithMessage("Product Is Not Exist");
		}
	}
}
