using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitCreateOrderResult : ICreateOrderResult
	{
		#region Public copnstructors



		public YobitCreateOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region ICreateOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
