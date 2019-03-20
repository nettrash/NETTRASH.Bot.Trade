using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitPair : IPair
	{
		#region Public properties



		public YOBIT.API.Data.Stock.Ticker Ticker { get; set; }



		#endregion
		#region Public constructors



		public YobitPair(string sName)
		{
			Name = sName;
		}



		#endregion
		#region Public methods



		public async Task InitializeAsync(YOBIT.API.Stock stockInstance, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
		{
			Ticker = await stockInstance.GetTickerAsync(Name, cancellationToken);
			YobitCurrency cSource = new YobitCurrency(Ticker);
			Source = cSource;
			Target = new YobitCurrency(Ticker, cSource);
		}



		#endregion
		#region IPair members



		public string Name { get; private set; }

		public ICurrency Source { get; private set; }

		public ICurrency Target { get; private set; }

		public decimal Buy { get { return (Ticker.BIOBTC?.Buy ?? Ticker.BIORUR?.Buy) ?? 0; } }

		public decimal Sell { get { return (Ticker.BIOBTC?.Sell ?? Ticker.BIORUR?.Sell) ?? 0; } }



		#endregion
	}
}
