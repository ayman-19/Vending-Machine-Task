using FluentValidation;
using TaskFlapKap.Application.Feather.Users.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Users.Commands.Validation
{
	public class DeleteUserValidation : AbstractValidator<DeleteUserRequest>
	{
		private readonly IUnitOfWork dbContext;

		public DeleteUserValidation(IUnitOfWork dbContext)
		{
			this.dbContext = dbContext;
			CustomValidation();
		}
		public void CustomValidation()
		{
			RuleFor(u => u.id)
			.MustAsync(async (s, CancellationToken) => await dbContext.Users.IsAnyExistAsync(u => u.Id.Equals(s))).WithMessage("User Is Not Exist");
		}
	}
}
