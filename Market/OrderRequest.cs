namespace NETTRASH.BOT.Trade.Market
{
	public class OrderRequest : IOrderRequest
	{
		#region Public properties



		public OrderType RequestType { get; set; }

		public IPair Pair { get; set; }

		public decimal Rate { get; set; }

		public decimal Amount { get; set; }



		#endregion
	}
}
