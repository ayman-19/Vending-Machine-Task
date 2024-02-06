namespace TaskFlapKap.Application.IRepositories
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository Users { get; }
		IProductRepository Products { get; }
		Task CommitAsync();
		Task RollbackAsync();
		Task<int> SaveChangesAsync();
	}
}
