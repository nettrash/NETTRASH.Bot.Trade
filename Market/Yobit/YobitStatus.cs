namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitStatus : IMarketStatus
	{
		#region IMarketStatus members



		public bool Active { get; private set; }



		#endregion
		#region Public constructors



		public YobitStatus(bool bStatus)
		{
			Active = bStatus;
		}



		#endregion
	}
}
