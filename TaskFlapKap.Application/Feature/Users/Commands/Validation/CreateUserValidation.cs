using FluentValidation;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Users.Commands.Validation
{
	public class CreateUserValidation : AbstractValidator<CreateUserRequest>
	{
		private readonly IUnitOfWork dbContext;

		public CreateUserValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}

		private void CustomValidation()
		{
			RuleFor(u => u.dto.UserName)
				.NotEmpty().WithMessage("UserName Is Not Empty")
				.NotNull().WithMessage("UserName Is Not Null")
				.MustAsync(async (m, CancellationToken) => !await dbContext.Users.IsAnyExistAsync(s => s.UserName!.Equals(m))).WithMessage("UserName Is Exist");

			RuleFor(u => u.dto.Password)
				.NotEmpty().WithMessage("Password Is Not Empty")
				.NotNull().WithMessage("Password Is Not Null");

			RuleFor(u => u.dto.ConfirmPassword)
				.NotEmpty().WithMessage("ConfirmPassword Is Not Empty")
				.NotNull().WithMessage("ConfirmPassword Is Not Null");
			RuleFor(u => u.dto.Role)
				.NotEmpty().WithMessage("Role Is Not Empty")
				.NotNull().WithMessage("Role Is Not Null");
		}
	}
}
