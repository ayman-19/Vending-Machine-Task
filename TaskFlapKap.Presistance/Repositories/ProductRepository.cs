using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Domain.Model;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private readonly ApplicationDbContext context;

		public ProductRepository(ApplicationDbContext context) : base(context)
		{
			this.context = context;
		}
		public Task<double> GetCostProductById(int id)
		{
			return Task.FromResult(context.Products.Where(p => p.Id == id)
				.AsQueryable().Select(p => p.Cost).FirstOrDefault());
		}
		public bool InValidProduct(Func<Product, bool> predicate)
		{
			return context.Products.Any(predicate);
		}
	}
}
