using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskFlapKap.Application.IAccountServices;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Presistance.AccountServices;
using TaskFlapKap.Presistance.DbContext;
using TaskFlapKap.Presistance.Repositories;

namespace TaskFlapKap.Presistance.Dependancies
{
	public static class DependancyInjection
	{
		public static IServiceCollection AddPresistance(this IServiceCollection services, string strConnection)
		{
			services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(strConnection));
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
			});
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IProductRepository, ProductRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IAccountService, AccountService>();
			return services;
		}
	}
}
