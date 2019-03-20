using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market
{
	public static class MarketFactory
	{
		#region Public static methods



		public static IMarket Create(MarketType typeOfMarket, Dictionary<string, string> settings)
		{
			switch (typeOfMarket)
			{
				case MarketType.Yobit:
					return new Yobit.YobitMarket(settings["Key"], settings["Secret"]);
				case MarketType.Bittrex:
					return null;
				case MarketType.ProfitBest:
					return null;
				default:
					throw new InvalidOperationException($"Market {typeOfMarket} unsupported.");
			}
		}



		#endregion
	}
}
