using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market
{
	public interface IPair
	{
		#region Interface properties



		string Name { get; }

		ICurrency Source { get; }

		ICurrency Target { get; }

		decimal Buy { get; }

		decimal Sell { get; }



		#endregion
	}
}
