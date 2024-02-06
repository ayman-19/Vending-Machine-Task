using FluentValidation;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Users.Commands.Validation
{
	public class UpdateUserValidation : AbstractValidator<UpdateUserRequest>
	{
		private readonly IUnitOfWork dbContext;

		public UpdateUserValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		private void CustomValidation()
		{
			RuleFor(u => u.dto.UserName)
				.NotEmpty().WithMessage("UserName Is Not Empty")
				.NotNull().WithMessage("UserName Is Not Null")
				.MustAsync(async (m, CancellationToken) => !await dbContext.Users.IsAnyExistAsync(s => s.UserName!.Equals(m) && s.Id != m)).WithMessage("UserName Is Exist");

			RuleFor(u => u.id)
				.MustAsync(async (s, CancellationToken) => await dbContext.Users.IsAnyExistAsync(u => u.Id.Equals(s))).WithMessage("User Is Not Exist");
			RuleFor(p => p.dto.Deposit).GreaterThan(0).WithMessage("no Enter Negative value!");
		}

	}
}
