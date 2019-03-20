using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexPair : IPair
	{
		#region Public properties



		public BITTREX.API.Data.Response<BITTREX.API.Data.Stock.Ticker> Pair { get; set; }



		#endregion
		#region Public constructors



		public BittrexPair(string sName)
		{
			Name = sName;
		}



		#endregion
		#region Public methods
		


		public async Task InitializeAsync(BITTREX.API.Stock stockInstance, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
		{
			Pair = await stockInstance.GetTickerAsync(Name, cancellationToken);
			BittrexCurrency cSource = new BittrexCurrency(this);
			Source = cSource;
			Target = new BittrexCurrency(this, cSource);
		}



		#endregion
		#region IPair members



		public string Name { get; private set; }

		public ICurrency Source { get; private set; }

		public ICurrency Target { get; private set; }

		public decimal Buy { get { return Pair.Data.BID; } }

		public decimal Sell { get { return Pair.Data.ASK; } }



		#endregion
	}
}
