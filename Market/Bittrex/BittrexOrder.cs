using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexOrder : IOrder
	{
		#region Public constructors



		public BittrexOrder(string sId, OrderType typeOfOrder, decimal nRate, decimal nAmount)
		{
			Id = sId;
			TypeOfOrder = typeOfOrder;
			Rate = nRate;
			Amount = nAmount;
		}



		#endregion
		#region IOrder members



		public string Id { get; private set; }

		public OrderType TypeOfOrder { get; private set; }

		public decimal Rate { get; private set; }

		public decimal Amount { get; private set; }



		#endregion
	}
}
