using System.ComponentModel.DataAnnotations.Schema;

namespace TaskFlapKap.Domain.Model
{
	public class Product
	{
		public int Id { get; set; }
		public int AmountAvailable { get; set; }
		public double Cost { get; set; }
		public string ProductName { get; set; }
		public string SellerId { get; set; }
		[ForeignKey(nameof(SellerId))]
		public User? User { get; set; }
	}
}
