using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Application.IRepositories
{
	public interface IProductRepository : IRepository<Product>
	{
		Task<double> GetCostProductById(int id);
		bool InValidProduct(Func<Product, bool> predicate);
	}
}
