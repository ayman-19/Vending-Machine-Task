using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Queries.Handler
{
	public class GetProductByIdHandler : IRequestHandler<GetProductByIdRequest, ProductResponse>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<GetAllProductHandler> logger;
		private readonly IMapper mapper;

		public GetProductByIdHandler(IUnitOfWork dbContext, ILogger<GetAllProductHandler> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;
		}
		public async Task<ProductResponse> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var product = await dbContext.Products.GetAsync(p => p.Id == request.id, ["User"]);
				logger.LogInformation($"Get Product {product.ProductName}");
				var result = mapper.Map<ProductResponse>(product);
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError("Product Not Found!" + ex.Message);
				return new();
			}
		}
	}
}
