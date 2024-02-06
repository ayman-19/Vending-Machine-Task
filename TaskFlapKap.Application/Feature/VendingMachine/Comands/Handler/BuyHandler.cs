using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TaskFlapKap.Application.Feather.VendingMachine.Comands.Request;
using TaskFlapKap.Application.Helper;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.DataTransfareObject.Product;

namespace TaskFlapKap.Application.Feather.VendingMachine.Comands.Handler
{
	internal class BuyHandler : IRequestHandler<BuyRequest, ResponseBuyProduct>
	{
		private readonly IUnitOfWork dbContext;
		private readonly IHttpContextAccessor _httpContext;

		public BuyHandler(IUnitOfWork dbContext, IHttpContextAccessor httpContext)
		{
			this.dbContext = dbContext;
			_httpContext = httpContext;
		}
		public async Task<ResponseBuyProduct> Handle(BuyRequest request, CancellationToken cancellationToken)
		{
			var userId = _httpContext.HttpContext.User.Claims.First(u => u.Type == ClaimTypes.PrimarySid).Value;
			var result = await BuyProccess(request.dtos, userId);
			if (!result.Products!.Any())
				throw new InvalidDataException("InValid Product!");
			return result;

		}
		private async Task<ResponseBuyProduct> BuyProccess(List<BuyProductdto> list, string userId)
		{
			double totalspint = 0;
			Parallel.ForEach(list, async product =>
			{
				var cost = await dbContext.Products.GetCostProductById(product.Id);
				if (cost != default)
					totalspint += cost * product.Amount;
				else
					list.Remove(product);
			});
			var user = await dbContext.Users.GetAsync(u => u.Id == userId, astracking: true);
			double change = user.Deposit - totalspint;
			if (change >= 0)
			{
				try
				{
					user.Deposit = change;
					foreach (var product in list)
					{
						var updateProduct = await dbContext.Products.GetAsync(p => p.Id == product.Id, astracking: true);
						if (product.Amount <= updateProduct.AmountAvailable)
							updateProduct.AmountAvailable -= product.Amount;
						else
						{
							var extract = product.Amount - updateProduct.AmountAvailable;
							updateProduct.AmountAvailable = 0;
							totalspint -= (extract * updateProduct.Cost);
							product.Amount -= extract;
						}
					}
					await dbContext.SaveChangesAsync();
					await dbContext.CommitAsync();
				}
				catch (Exception ex)
				{
					await dbContext.RollbackAsync();
				}
			}
			else
			{
				return new ResponseBuyProduct { Message = $"{user.UserName} doesn't have enough coins" };
			}

			return new ResponseBuyProduct { IsSuccess = true, Products = list, TotalSpintAfterChange = ChangeCoins.CalcCoins(totalspint) };
		}
	}
}
