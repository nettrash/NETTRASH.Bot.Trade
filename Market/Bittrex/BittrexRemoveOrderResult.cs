namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexRemoveOrderResult : IRemoveOrderResult
	{
		#region Public copnstructors



		public BittrexRemoveOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region IRemoveOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
