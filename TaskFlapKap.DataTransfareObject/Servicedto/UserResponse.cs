namespace TaskFlapKap.DataTransfareObject.Servicedto
{
	public class UserResponse
	{
		public string UserName { get; set; }
		public string Accesstoken { get; set; }
		public double Deposit { get; set; }
		public string Role { get; set; }
		public string Massage { get; set; }
		public bool IsAuthenticated { get; set; }
	}
}
