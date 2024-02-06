using FluentValidation;
using TaskFlapKap.Application.Feather.Users.Queries.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Users.Queries.Validation
{
	public class LoginValidation : AbstractValidator<LoginRequest>
	{
		private readonly IUnitOfWork dbContext;

		public LoginValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValiation();
		}
		private void CustomValiation()
		{
			RuleFor(u => u.dto.UserName)
				.NotEmpty().WithMessage("UserName Is Not Empty")
				.NotNull().WithMessage("UserName Is Not Null")
				.MustAsync(async (m, CancellationToken) => await dbContext.Users.IsAnyExistAsync(s => s.UserName!.Equals(m))).WithMessage("UserName Is Exist");


			RuleFor(u => u.dto.Password)
				.NotNull().WithMessage("Password Is Not Null")
				.NotEmpty().WithMessage("Password Is Not Empty");
		}

	}
}
