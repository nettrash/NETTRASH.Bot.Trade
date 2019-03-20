using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market
{
	public interface IMarket
	{
		#region Interface properties



		MarketType MarketType { get; }



		#endregion
		#region Interface Methods



		Task<IMarketStatus> StatusAsync(System.Threading.CancellationToken cancellationToken);

		Task<IEnumerable<IPair>> PairsAsync(System.Threading.CancellationToken cancellationToken);

		Task<IPair> PairAsync(string Name, System.Threading.CancellationToken cancellationToken);

		Task<IEnumerable<IOrder>> OrdersAsync(IPair pair, bool bOnlyOwned, System.Threading.CancellationToken cancellationToken);

		Task<IOrder> OrderAsync(IPair pair, string Id, System.Threading.CancellationToken cancellationToken);

		Task<ICreateOrderResult> CreateOrderAsync(IOrderRequest request, System.Threading.CancellationToken cancellationToken);

		Task<IRemoveOrderResult> RemoveOrderAsync(string Id, System.Threading.CancellationToken cancellationToken);



		#endregion
	}
}
