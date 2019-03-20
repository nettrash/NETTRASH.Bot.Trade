using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestPair : IPair
	{
		#region Public properties



		public PROFITBEST.API.Data.TradePair Pair { get; set; }



		#endregion
		#region Public constructors



		public ProfitBestPair(string sName)
		{
			Name = sName;
		}



		#endregion
		#region Public methods



		public async Task InitializeAsync(PROFITBEST.API.Trade tradeInstance, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
		{
			Pair = await tradeInstance.GetPairAsync(Name, cancellationToken);
			ProfitBestCurrency cSource = new ProfitBestCurrency(Pair);
			Source = cSource;
			Target = new ProfitBestCurrency(Pair, cSource);
		}



		#endregion
		#region IPair members



		public string Name { get; private set; }

		public ICurrency Source { get; private set; }

		public ICurrency Target { get; private set; }

		public decimal Buy { get { return decimal.Parse(Pair.LastPrice, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture); } }

		public decimal Sell { get { return decimal.Parse(Pair.LastPrice, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture); } }



		#endregion
	}
}
