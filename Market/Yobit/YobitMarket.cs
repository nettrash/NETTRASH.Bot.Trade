using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.SqlClient;

namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitMarket : IMarket
	{
		#region Private properties



		private YOBIT.API.Stock _stock { get; set; }

		private YOBIT.API.Trade _trade { get; set; }



		#endregion
		#region Public constructors



		public YobitMarket()
		{
			_stock = new YOBIT.API.Stock();
			_trade = new YOBIT.API.Trade();
		}

		public YobitMarket(string sKey, string sSecret)
		{
			_stock = new YOBIT.API.Stock(sKey, sSecret);
			_trade = new YOBIT.API.Trade(sKey, sSecret);
		}



		#endregion
		#region Private methods



		private async Task<int> _getNonceAsync(System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
		{
			using (SqlConnection conn = new SqlConnection(Properties.Settings.Default.DBConnection))
			{
				await conn.OpenAsync(cancellationToken);
				using (SqlCommand cmd = new SqlCommand("Seq_Get", conn))
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;

					cmd.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 50).Direction = System.Data.ParameterDirection.Input;
					cmd.Parameters["@Name"].Value = "YoBit";

					cmd.Parameters.Add("@Value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

					await cmd.ExecuteNonQueryAsync(cancellationToken);

					return (int)cmd.Parameters["@Value"].Value;
				}
			}
		}



		#endregion
		#region IMarket members



		public MarketType MarketType => MarketType.Yobit;

		public async Task<ICreateOrderResult> CreateOrderAsync(IOrderRequest request, System.Threading.CancellationToken cancellationToken)
		{
			YOBIT.API.Data.Market.Response<YOBIT.API.Data.Market.Order> order = await _trade.CreateOrderAsync(await _getNonceAsync(), request.RequestType.ToString(), request.Pair.Name, request.Rate, request.Amount, cancellationToken);
			return new YobitCreateOrderResult(order.Success > 0);
		}

		public async Task<IOrder> OrderAsync(IPair pair, string Id, System.Threading.CancellationToken cancellationToken)
		{
			if (!pair.Name.Equals("bio_btc") && !pair.Name.Equals("bio_rur"))
				throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			YOBIT.API.Data.Stock.OrderBook orders = await _stock.GetOrderBookAsync(pair.Name, cancellationToken);
			IEnumerable<YobitOrder> asks = orders.BIOBTC.ASKS.GroupBy(rate => rate[0]).Select(g => new YobitOrder(g.Key.ToString(), OrderType.sell, g.Key, orders.BIOBTC.ASKS.Where(rate => rate[0] == g.Key).Sum(o => o[1])));
			IOrder order = asks.Where(o => o.Id.Equals(Id)).FirstOrDefault();
			if (order != null)
				return order;
			IEnumerable<YobitOrder> bids = orders.BIOBTC.BIDS.GroupBy(rate => rate[0]).Select(g => new YobitOrder(g.Key.ToString(), OrderType.buy, g.Key, orders.BIOBTC.BIDS.Where(rate => rate[0] == g.Key).Sum(o => o[1])));
			order = bids.Where(o => o.Id.Equals(Id)).FirstOrDefault();
			return order;
		}

		public async Task<IEnumerable<IOrder>> OrdersAsync(IPair pair, bool bOnlyOwned, System.Threading.CancellationToken cancellationToken)
		{
			if (!pair.Name.Equals("bio_btc") && !pair.Name.Equals("bio_rur"))
				throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			YOBIT.API.Data.Stock.OrderBook orders = await _stock.GetOrderBookAsync(pair.Name, cancellationToken);
			IEnumerable<YobitOrder> asks = orders.BIOBTC.ASKS.GroupBy(rate => rate[0]).Select(g => new YobitOrder(g.Key.ToString(), OrderType.sell, g.Key, orders.BIOBTC.ASKS.Where(rate => rate[0] == g.Key).Sum(o => o[1])));
			IEnumerable<YobitOrder> bids = orders.BIOBTC.BIDS.GroupBy(rate => rate[0]).Select(g => new YobitOrder(g.Key.ToString(), OrderType.buy, g.Key, orders.BIOBTC.BIDS.Where(rate => rate[0] == g.Key).Sum(o => o[1])));
			return asks.Concat(bids);
		}

		public async Task<IEnumerable<IPair>> PairsAsync(System.Threading.CancellationToken cancellationToken)
		{
			YobitPair pairBIOBTC = new YobitPair("bio_btc");
			await pairBIOBTC.InitializeAsync(_stock, cancellationToken);
			YobitPair pairBIORUR = new YobitPair("bio_rur");
			await pairBIORUR.InitializeAsync(_stock, cancellationToken);
			return new IPair[] { pairBIOBTC, pairBIORUR };
		}

		public async Task<IPair> PairAsync(string Name, System.Threading.CancellationToken cancellationToken)
		{
			YobitPair pair = new YobitPair(Name);
			await pair.InitializeAsync(_stock, cancellationToken);
			return pair;
		}

		public async Task<IMarketStatus> StatusAsync(System.Threading.CancellationToken cancellationToken)
		{
			YobitPair pair = new YobitPair("bio_btc");
			await pair.InitializeAsync(_stock, cancellationToken);
			return new YobitStatus(pair?.Ticker?.BIOBTC != null && pair.Sell > 0 && pair.Buy > 0);
		}

		public async Task<IRemoveOrderResult> RemoveOrderAsync(string Id, System.Threading.CancellationToken cancellationToken)
		{
			return new YobitRemoveOrderResult(false);
		}



		#endregion
	}
}
