using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Products.Commands.Request;
using TaskFlapKap.Application.IRepositories;

namespace TaskFlapKap.Application.Feather.Products.Commands.Handler
{
	public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, string>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<CreateProductHandler> logger;

		public DeleteProductHandler(IUnitOfWork dbContext, ILogger<CreateProductHandler> logger)
		{
			this.dbContext = dbContext;
			this.logger = logger;
		}
		public async Task<string> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var product = await dbContext.Products.GetAsync(p => p.Id.Equals(request.id), astracking: false);
				await dbContext.Products.Delete(product);
				await dbContext.SaveChangesAsync();
				await dbContext.CommitAsync();
				logger.LogInformation($"{product.ProductName} Deleted");
				return "Deleted";
			}
			catch (Exception ex)
			{
				await dbContext.RollbackAsync();
				logger.LogError(ex.Message);
				return ex.Message;
			}
		}
	}
}
