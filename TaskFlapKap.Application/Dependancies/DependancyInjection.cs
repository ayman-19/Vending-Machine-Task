using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TaskFlapKap.Application.Behevior;

namespace TaskFlapKap.Application.Dependancies
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));
			services.AddAutoMapper(typeof(DependancyInjection).Assembly);
			services.AddScoped(typeof(IPipelineBehavior<,>), typeof(BeheviorValidation<,>));
			services.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly);
			return services;
		}
	}
}
