using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TaskFlapKap.Application.Feather.Products.Queries.Request;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.Products.Queries.Handler
{
	public class GetAllProductHandler : IRequestHandler<GatAllProductRequest, List<ProductResponse>>
	{
		private readonly IUnitOfWork dbContext;
		private readonly ILogger<GetAllProductHandler> logger;
		private readonly IMapper mapper;

		public GetAllProductHandler(IUnitOfWork dbContext, ILogger<GetAllProductHandler> logger, IMapper mapper)
		{
			this.dbContext = dbContext;
			this.logger = logger;
			this.mapper = mapper;
		}
		public async Task<List<ProductResponse>> Handle(GatAllProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				logger.LogInformation("Get All Product!");
				var products = (await dbContext.Products.GetAllAsync(includes: ["User"])).ToList();
				var result = mapper.Map<List<ProductResponse>>(products);
				return result;
			}
			catch (Exception ex)
			{
				logger.LogError(ex.Message);
				return new List<ProductResponse>();
			}
		}
	}
}
