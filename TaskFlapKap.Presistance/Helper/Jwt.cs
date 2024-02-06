namespace TaskFlapKap.Presistance.Helper
{
	public class Jwt
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public double AccessTokenExiretionDate { get; set; }
	}
}
