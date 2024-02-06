using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Products.Commands.Handler
{
	public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, string>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<CreateProductHandler> logger;

		public UpdateProductHandler(IUnitOfWork dbContext, ILogger<CreateProductHandler> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var product = await dbContext.Products.GetAsync(p => p.Id.Equals(request.id), astracking: true);
				product.Cost = request.Product.Cost == default ? product.Cost : request.Product.Cost;
				product.AmountAvailable += request.Product.AmountAvailable;
				await dbContext.SaveChangesAsync();
				await dbContext.CommitAsync();
				logger.LogInformation($"{product.ProductName} Updated!");
				return "Success";
			}
			catch (Exception ex)
			{
				await dbContext.RollbackAsync();
				logger.LogError("Not Updated" + ex.Message);
				return "Failed";
			}
		}
	}
}
