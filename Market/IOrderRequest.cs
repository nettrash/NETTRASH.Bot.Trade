using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market
{
	public interface IOrderRequest
	{
		#region Interface properties



		OrderType RequestType { get; }

		IPair Pair { get; }

		decimal Rate { get; }

		decimal Amount { get; }



		#endregion
	}
}
