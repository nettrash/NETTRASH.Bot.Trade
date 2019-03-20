namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexCurrency : ICurrency
	{
		#region Private consts



		private const string m_csBIO = "BIO";

		private const string m_csBTC = "BTC";

		private const string m_csRUR = "RUR";

		private const string m_csETH = "ETH";

		private const string m_csSIB = "SIB";



		#endregion
		#region Public constructors



		public BittrexCurrency()
		{

		}

		public BittrexCurrency(BittrexPair pair)
		{
			if (pair != null && pair.Name.StartsWith("BIO"))
			{
				Name = m_csBIO;
			}
			if (pair != null && pair.Name.StartsWith("BTC"))
			{
				Name = m_csBTC;
			}
			if (pair != null && pair.Name.StartsWith("RUR"))
			{
				Name = m_csRUR;
			}
			if (pair != null && pair.Name.StartsWith("ETH"))
			{
				Name = m_csETH;
			}
			if (pair != null && pair.Name.StartsWith("SIB"))
			{
				Name = m_csSIB;
			}
		}

		public BittrexCurrency(BittrexPair pair, BittrexCurrency source)
		{
			if (pair != null && pair.Name.Contains("-BIO"))
			{
				Name = m_csBIO;
			}
			if (pair != null && pair.Name.Contains("-BTC"))
			{
				Name = m_csBTC;
			}
			if (pair != null && pair.Name.Contains("-RUR"))
			{
				Name = m_csRUR;
			}
			if (pair != null && pair.Name.Contains("-ETH"))
			{
				Name = m_csETH;
			}
			if (pair != null && pair.Name.Contains("-SIB"))
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
