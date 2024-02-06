using Microsoft.AspNetCore.Identity;
using TaskFlapKap.Domain.Enum;

namespace TaskFlapKap.Domain.Model
{
	public class User : IdentityUser
	{
		public Role Role { get; set; }
		public double Deposit { get; set; }
		public List<Product>? Products { get; set; }
	}
}
