using System.ComponentModel.DataAnnotations;
using TaskFlapKap.Domain.Enum;

namespace TaskFlapKap.DataTransfareObject.Servicedto
{
	public class Register
	{
		public string UserName { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[DataType(DataType.Password), Compare("Password")]
		public string ConfirmPassword { get; set; }
		public Role Role { get; set; }
	}
}
