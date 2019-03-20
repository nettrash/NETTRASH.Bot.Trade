namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexStatus : IMarketStatus
	{
		#region IMarketStatus members



		public bool Active { get; private set; }



		#endregion
		#region Public constructors



		public BittrexStatus(bool bStatus)
		{
			Active = bStatus;
		}



		#endregion
	}
}
