namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitCurrency : ICurrency
	{
		#region Private consts



		private const string m_csBIO = "BIO";

		private const string m_csBTC = "BTC";

		private const string m_csRUR = "RUR";



		#endregion
		#region Public constructors



		public YobitCurrency()
		{

		}

		public YobitCurrency(YOBIT.API.Data.Stock.Ticker ticker)
		{
			if (ticker?.BIOBTC != null || ticker?.BIORUR != null)
			{
				Name = m_csBIO;
			}
		}

		public YobitCurrency(YOBIT.API.Data.Stock.Ticker ticker, YobitCurrency source)
		{
			if (ticker?.BIOBTC != null && source.Name.Equals(m_csBIO))
			{
				Name = m_csBTC;
			}
			if (ticker?.BIORUR != null && source.Name.Equals(m_csBIO))
			{
				Name = m_csRUR;
			}
		}



		#endregion
		#region ICurrency members



		public string Name { get; }



		#endregion
	}
}
