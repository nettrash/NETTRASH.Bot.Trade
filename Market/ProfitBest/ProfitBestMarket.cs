using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestMarket : IMarket
	{
		#region Private properties



		private static NLog.Logger _logger => NLog.LogManager.GetCurrentClassLogger();

		private PROFITBEST.API.Trade _market { get; set; }



		#endregion
		#region Public constructors



		public ProfitBestMarket()
		{
			_market = new PROFITBEST.API.Trade();
		}

		public ProfitBestMarket(string sKey)
		{
			_market = new PROFITBEST.API.Trade(sKey);
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
					cmd.Parameters["@Name"].Value = "ProfitBest";

					cmd.Parameters.Add("@Value", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.Output;

					await cmd.ExecuteNonQueryAsync(cancellationToken);

					return (int)cmd.Parameters["@Value"].Value;
				}
			}
		}

		private bool _isValidPair(IPair pair)
		{
			return 
				pair.Name.Equals("BIO-BTC") || 
				pair.Name.Equals("BIO-RUR") || 
				pair.Name.Equals("BIO-ETH") ||
				pair.Name.Equals("BIO-SIB");
		}



		#endregion
		#region IMarket members



		public MarketType MarketType => MarketType.ProfitBest;

		public async Task<ICreateOrderResult> CreateOrderAsync(IOrderRequest request, System.Threading.CancellationToken cancellationToken)
		{
			List<PROFITBEST.API.Data.TradeBaseOrder> stock = new List<PROFITBEST.API.Data.TradeBaseOrder>();
			stock.Add(new PROFITBEST.API.Data.TradeBaseOrder
			{
				Pair = request.Pair.Name,
				Amount = request.Amount.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture),
				Price = request.Rate.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture),
				Type = request.RequestType == OrderType.buy ? PROFITBEST.API.Data.TradeOrderType.buy : PROFITBEST.API.Data.TradeOrderType.sell
			});
			_logger.Trace($"Request: {Newtonsoft.Json.JsonConvert.SerializeObject(stock)}");
			PROFITBEST.API.Data.TradeMyOrder[] orders = await _market.SetOrdersAsync(stock.ToArray());
			_logger.Trace($"Response: {Newtonsoft.Json.JsonConvert.SerializeObject(orders)}");
			return new ProfitBestCreateOrderResult(orders != null && orders.Length > 0 && orders[0].Id > 0);
		}

		public async Task<IOrder> OrderAsync(IPair pair, string Id, System.Threading.CancellationToken cancellationToken)
		{
			if (!_isValidPair(pair))
				throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			IEnumerable<IOrder> orders = await OrdersAsync(pair, false, cancellationToken);
			return orders.Where(o => o.Id.Equals(Id)).FirstOrDefault();
		}

		public async Task<IEnumerable<IOrder>> OrdersAsync(IPair pair, bool bOnlyOwned, System.Threading.CancellationToken cancellationToken)
		{
			if (!_isValidPair(pair))
				throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			if (!bOnlyOwned)
			{
				PROFITBEST.API.Data.TradeOrder[] orders = await _market.GetOrdersAsync($"{pair.Source.Name.ToUpper()}-{pair.Target.Name.ToUpper()}", cancellationToken);
				IEnumerable<ProfitBestOrder> asks = orders.Where(o => o.Type == PROFITBEST.API.Data.TradeOrderType.sell).GroupBy(rate => rate.Price).Select(g => new ProfitBestOrder(g.Key.ToString(), OrderType.sell, decimal.Parse(g.Key, System.Globalization.NumberStyles.AllowDecimalPoint), orders.Where(rate => rate.Price == g.Key && rate.Type == PROFITBEST.API.Data.TradeOrderType.sell).Sum(o => decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint))));
				IEnumerable<ProfitBestOrder> bids = orders.Where(o => o.Type == PROFITBEST.API.Data.TradeOrderType.buy).GroupBy(rate => rate.Price).Select(g => new ProfitBestOrder(g.Key.ToString(), OrderType.buy, decimal.Parse(g.Key, System.Globalization.NumberStyles.AllowDecimalPoint), orders.Where(rate => rate.Price == g.Key && rate.Type == PROFITBEST.API.Data.TradeOrderType.buy).Sum(o => decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint))));
				return asks.Concat(bids);
			}
			PROFITBEST.API.Data.TradeMyOrder[] open = await _market.GetMyOrdersAsync($"{pair.Source.Name.ToUpper()}-{pair.Target.Name.ToUpper()}", PROFITBEST.API.Data.TradeOrderStatus.open, cancellationToken);
			return open.Select(o => new ProfitBestOrder(o.Id.ToString(), (o.Type == PROFITBEST.API.Data.TradeOrderType.buy ? OrderType.buy : OrderType.sell), decimal.Parse(o.Price, System.Globalization.NumberStyles.AllowDecimalPoint), decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint)));
		}

		public async Task<IEnumerable<IPair>> PairsAsync(System.Threading.CancellationToken cancellationToken)
		{
			ProfitBestPair pairBIOBTC = new ProfitBestPair("BIO-BTC");
			await pairBIOBTC.InitializeAsync(_market, cancellationToken);
			ProfitBestPair pairBIORUR = new ProfitBestPair("BIO-RUR");
			await pairBIORUR.InitializeAsync(_market, cancellationToken);
			ProfitBestPair pairBIOETH = new ProfitBestPair("BIO-ETH");
			await pairBIOETH.InitializeAsync(_market, cancellationToken);
			ProfitBestPair pairBIOSIB = new ProfitBestPair("BIO-SIB");
			await pairBIOSIB.InitializeAsync(_market, cancellationToken);
			return new IPair[] { pairBIOBTC, pairBIORUR, pairBIOETH, pairBIOSIB };
		}

		public async Task<IPair> PairAsync(string Name, System.Threading.CancellationToken cancellationToken)
		{
			ProfitBestPair pair = new ProfitBestPair(Name);
			await pair.InitializeAsync(_market, cancellationToken);
			return pair;
		}

		public async Task<IMarketStatus> StatusAsync(System.Threading.CancellationToken cancellationToken)
		{
			ProfitBestPair pair = new ProfitBestPair("BIO-BTC");
			await pair.InitializeAsync(_market, cancellationToken);
			return new ProfitBestStatus(pair?.Pair != null && pair.Sell > 0 && pair.Buy > 0);
		}

		public async Task<IRemoveOrderResult> RemoveOrderAsync(string Id, System.Threading.CancellationToken cancellationToken)
		{
			PROFITBEST.API.Data.TradeOrderStatusInfo[] retVal = await _market.DeleteOrdersAsync(new ulong[] { ulong.Parse(Id) }, cancellationToken);
			return new ProfitBestRemoveOrderResult(retVal != null && retVal.Length.Equals(1) && retVal[0].Status == PROFITBEST.API.Data.TradeOrderStatus.canceled);
		}



		#endregion
	}
}
