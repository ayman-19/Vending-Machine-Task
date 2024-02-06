using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.Feather.Products.Commands.Handler
{
	public class CreateProductHandler : IRequestHandler<CreateProductRequest, string>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<CreateProductHandler> logger;
		private readonly IMapper mapper;
		private readonly IHttpContextAccessor _httpContext;

		public CreateProductHandler(IUnitOfWork dbContext, ILogger<CreateProductHandler> logger, IMapper mapper, IHttpContextAccessor httpContext)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;
			_httpContext = httpContext;
		}
		public async Task<string> Handle(CreateProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var map = mapper.Map<Product>(request.Product);
				map.SellerId = _httpContext.HttpContext.User.Claims.First(x => x.Type == ClaimTypes.PrimarySid).Value;
				await dbContext.Products.AddAsync(map);
				await dbContext.SaveChangesAsync();
				await dbContext.CommitAsync();
				logger.LogInformation($"{request.Product.ProductName} Retrived!");
				return "Success";
			}
			catch (Exception ex)
			{
				await dbContext.RollbackAsync();
				logger.LogError(ex.Message);
				return "Failed";
			}
		}
	}
}
