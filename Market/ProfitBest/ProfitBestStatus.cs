namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestStatus : IMarketStatus
	{
		#region IMarketStatus members



		public bool Active { get; private set; }



		#endregion
		#region Public constructors



		public ProfitBestStatus(bool bStatus)
		{
			Active = bStatus;
		}



		#endregion
	}
}
