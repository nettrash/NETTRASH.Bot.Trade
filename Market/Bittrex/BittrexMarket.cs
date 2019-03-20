using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexMarket : IMarket
	{
		#region Private properties



		private static NLog.Logger _logger => NLog.LogManager.GetCurrentClassLogger();

		private BITTREX.API.Trade _trade { get; set; }

		private BITTREX.API.Stock _stock { get; set; }



		#endregion
		#region Public constructors



		public BittrexMarket()
		{
			_trade = new BITTREX.API.Trade();
			_stock = new BITTREX.API.Stock();
		}

		public BittrexMarket(string sKey, string sSecret)
		{
			_trade = new BITTREX.API.Trade(sKey, sSecret);
			_stock = new BITTREX.API.Stock(sKey, sSecret);
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
					cmd.Parameters["@Name"].Value = "Bittrex";

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
				pair.Name.Equals("BIO-ETH");
		}



		#endregion
		#region IMarket members



		public MarketType MarketType => MarketType.Bittrex;

		public async Task<ICreateOrderResult> CreateOrderAsync(IOrderRequest request, System.Threading.CancellationToken cancellationToken)
		{
			throw new InvalidOperationException("Not support at this time");
			//List<Bittrex.API.Data.TradeBaseOrder> stock = new List<Bittrex.API.Data.TradeBaseOrder>();
			//stock.Add(new Bittrex.API.Data.TradeBaseOrder
			//{
			//	Pair = request.Pair.Name,
			//	Amount = request.Amount.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture),
			//	Price = request.Rate.ToString("0.00000000", System.Globalization.CultureInfo.InvariantCulture),
			//	Type = request.RequestType == OrderType.buy ? Bittrex.API.Data.TradeOrderType.buy : Bittrex.API.Data.TradeOrderType.sell
			//});
			//_logger.Trace($"Request: {Newtonsoft.Json.JsonConvert.SerializeObject(stock)}");
			//Bittrex.API.Data.TradeMyOrder[] orders = await _market.SetOrdersAsync(stock.ToArray());
			//_logger.Trace($"Response: {Newtonsoft.Json.JsonConvert.SerializeObject(orders)}");
			//return new BittrexCreateOrderResult(orders != null && orders.Length > 0 && orders[0].Id > 0);
		}

		public async Task<IOrder> OrderAsync(IPair pair, string Id, System.Threading.CancellationToken cancellationToken)
		{
			throw new InvalidOperationException("Not support at this time");
			//if (!_isValidPair(pair))
			//	throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			//IEnumerable<IOrder> orders = await OrdersAsync(pair, false, cancellationToken);
			//return orders.Where(o => o.Id.Equals(Id)).FirstOrDefault();
		}

		public async Task<IEnumerable<IOrder>> OrdersAsync(IPair pair, bool bOnlyOwned, System.Threading.CancellationToken cancellationToken)
		{
			throw new InvalidOperationException("Not support at this time");
			//if (!_isValidPair(pair))
			//	throw new InvalidOperationException($"Pair {pair.Name} not supported.");
			//if (!bOnlyOwned)
			//{
			//	Bittrex.API.Data.TradeOrder[] orders = await _market.GetOrdersAsync($"{pair.Source.Name.ToUpper()}-{pair.Target.Name.ToUpper()}", cancellationToken);
			//	IEnumerable<BittrexOrder> asks = orders.Where(o => o.Type == Bittrex.API.Data.TradeOrderType.sell).GroupBy(rate => rate.Price).Select(g => new BittrexOrder(g.Key.ToString(), OrderType.sell, decimal.Parse(g.Key, System.Globalization.NumberStyles.AllowDecimalPoint), orders.Where(rate => rate.Price == g.Key && rate.Type == Bittrex.API.Data.TradeOrderType.sell).Sum(o => decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint))));
			//	IEnumerable<BittrexOrder> bids = orders.Where(o => o.Type == Bittrex.API.Data.TradeOrderType.buy).GroupBy(rate => rate.Price).Select(g => new BittrexOrder(g.Key.ToString(), OrderType.buy, decimal.Parse(g.Key, System.Globalization.NumberStyles.AllowDecimalPoint), orders.Where(rate => rate.Price == g.Key && rate.Type == Bittrex.API.Data.TradeOrderType.buy).Sum(o => decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint))));
			//	return asks.Concat(bids);
			//}
			//Bittrex.API.Data.TradeMyOrder[] open = await _market.GetMyOrdersAsync($"{pair.Source.Name.ToUpper()}-{pair.Target.Name.ToUpper()}", Bittrex.API.Data.TradeOrderStatus.open, cancellationToken);
			//return open.Select(o => new BittrexOrder(o.Id.ToString(), (o.Type == Bittrex.API.Data.TradeOrderType.buy ? OrderType.buy : OrderType.sell), decimal.Parse(o.Price, System.Globalization.NumberStyles.AllowDecimalPoint), decimal.Parse(o.Amount, System.Globalization.NumberStyles.AllowDecimalPoint)));
		}

		public async Task<IEnumerable<IPair>> PairsAsync(System.Threading.CancellationToken cancellationToken)
		{
			BittrexPair pairBIOBTC = new BittrexPair("BIO-BTC");
			await pairBIOBTC.InitializeAsync(_stock, cancellationToken);
			BittrexPair pairBIORUR = new BittrexPair("BIO-RUR");
			await pairBIORUR.InitializeAsync(_stock, cancellationToken);
			BittrexPair pairBTCETH = new BittrexPair("BTC-ETH");
			await pairBTCETH.InitializeAsync(_stock, cancellationToken);
			BittrexPair pairBTCSIB = new BittrexPair("BTC-SIB");
			await pairBTCSIB.InitializeAsync(_stock, cancellationToken);
			return new IPair[] { pairBIOBTC, pairBIORUR, pairBTCETH, pairBTCSIB };
		}

		public async Task<IPair> PairAsync(string Name, System.Threading.CancellationToken cancellationToken)
		{
			BittrexPair pair = new BittrexPair(Name);
			await pair.InitializeAsync(_stock, cancellationToken);
			return pair;
		}

		public async Task<IMarketStatus> StatusAsync(System.Threading.CancellationToken cancellationToken)
		{
			BittrexPair pair = new BittrexPair("BTC-ETH");
			await pair.InitializeAsync(_stock, cancellationToken);
			return new BittrexStatus(pair?.Pair != null && pair.Sell > 0 && pair.Buy > 0);
		}

		public async Task<IRemoveOrderResult> RemoveOrderAsync(string Id, System.Threading.CancellationToken cancellationToken)
		{
			throw new InvalidOperationException("Not support at this time");
			//Bittrex.API.Data.TradeOrderStatusInfo[] retVal = await _market.DeleteOrdersAsync(new ulong[] { ulong.Parse(Id) }, cancellationToken);
			//return new BittrexRemoveOrderResult(retVal != null && retVal.Length.Equals(1) && retVal[0].Status == Bittrex.API.Data.TradeOrderStatus.canceled);
		}



		#endregion
	}
}
