using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskFlapKap.Domain.Model;

namespace TaskFlapKap.Presistance.DbContext
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<User>().ToTable("Users");
			builder.Entity<IdentityRole>().HasData(
				new IdentityRole
				{

					ConcurrencyStamp = Guid.NewGuid().ToString(),
					Id = Guid.NewGuid().ToString(),
					Name = "Buyer",
					NormalizedName = "buyer".ToUpper(),
				},
				new IdentityRole
				{
					ConcurrencyStamp = Guid.NewGuid().ToString(),
					Id = Guid.NewGuid().ToString(),
					Name = "Seller",
					NormalizedName = "Seller".ToUpper(),
				});
		}
		public DbSet<Product> Products { get; set; }
	}
}
