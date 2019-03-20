using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market
{
	public interface IOrder
	{
		#region Interface properties



		string Id { get; }

		OrderType TypeOfOrder { get; }

		decimal Rate { get; }

		decimal Amount { get; }



		#endregion
	}
}
