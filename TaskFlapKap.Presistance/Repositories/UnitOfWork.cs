using Microsoft.EntityFrameworkCore.Storage;
using TaskFlapKap.Application.IRepositories;
using TaskFlapKap.Presistance.DbContext;

namespace TaskFlapKap.Presistance.Repositories
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDbContextTransaction _dbContextTransaction;
		private readonly ApplicationDbContext _context;

		public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IProductRepository productRepository)
		{
			_context = context;
			_dbContextTransaction = _context.Database.BeginTransaction();
			Users = userRepository;
			Products = productRepository;
		}

		public async Task CommitAsync() => await _dbContextTransaction.CommitAsync();

		public async Task Dispose() => await _context.DisposeAsync();

		public async Task RollbackAsync() => await _dbContextTransaction.RollbackAsync();

		public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

		void IDisposable.Dispose() =>
			_dbContextTransaction.Dispose();

		public IUserRepository Users { get; }

		public IProductRepository Products { get; }

	}
}
