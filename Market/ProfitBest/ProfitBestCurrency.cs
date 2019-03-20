namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestCurrency : ICurrency
	{
		#region Private consts



		private const string m_csBIO = "BIO";

		private const string m_csBTC = "BTC";

		private const string m_csRUR = "RUR";

		private const string m_csETH = "ETH";

		private const string m_csSIB = "SIB";



		#endregion
		#region Public constructors



		public ProfitBestCurrency()
		{

		}

		public ProfitBestCurrency(PROFITBEST.API.Data.TradePair pair)
		{
			if (pair != null)
			{
				Name = m_csBIO;
			}
		}

		public ProfitBestCurrency(PROFITBEST.API.Data.TradePair pair, ProfitBestCurrency source)
		{
			if (pair != null && source.Name.Equals(m_csBIO) && pair.Name.Contains("BTC"))
			{
				Name = m_csBTC;
			}
			if (pair != null && source.Name.Equals(m_csBIO) && pair.Name.Contains("RUR"))
			{
				Name = m_csRUR;
			}
			if (pair != null && source.Name.Equals(m_csBIO) && pair.Name.Contains("ETH"))
			{
				Name = m_csETH;
			}
			if (pair != null && source.Name.Equals(m_csBIO) && pair.Name.Contains("SIB"))
			{
				Name = m_csSIB;
			}
		}



		#endregion
		#region ICurrency members



		public string Name { get; }



		#endregion
	}
}
