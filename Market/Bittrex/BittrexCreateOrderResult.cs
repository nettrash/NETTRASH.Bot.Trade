using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Bittrex
{
	public class BittrexCreateOrderResult : ICreateOrderResult
	{
		#region Public copnstructors



		public BittrexCreateOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region ICreateOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
