namespace TaskFlapKap.DataTransfareObject.Product
{
	public class ResponseBuyProduct
	{
		public List<BuyProductdto>? Products { get; set; }
		public double TotalSpintAfterChange { get; set; }
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
	}
}
