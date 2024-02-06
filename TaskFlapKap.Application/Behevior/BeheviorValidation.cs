using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;

namespace TaskFlapKap.Application.Behevior
{
	internal class BeheviorValidation<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public BeheviorValidation(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			if (_validators.Any())
			{
				var context = new ValidationContext<TRequest>(request);
				var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context)));
				var feliar = validationResults.SelectMany(e => e.Errors);
				if (feliar.Count() > 0)
				{
					var errors = feliar.Select(x => x.ErrorMessage);
					throw new ValidationException(string.Join(" - ", errors));
				}
			}
			return await next();
		}
	}
}
