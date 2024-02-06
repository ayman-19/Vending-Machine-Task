using System.Linq.Expressions;

namespace TaskFlapKap.Application.IRepositories
{
	public interface IRepository<TEntity> where TEntity : class
	{
		Task AddAsync(TEntity entity);
		Task Update(TEntity entity);
		Task Delete(TEntity entity);
		Task<bool> IsAnyExistAsync(Expression<Func<TEntity, bool>> pridecate);
		Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> pridecate = default!, string[] includes = default!, bool astracking = default);
		Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> pridecate = default!, string[] includes = default!, bool astracking = default);
	}
}
