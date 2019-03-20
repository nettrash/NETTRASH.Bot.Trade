using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.Yobit
{
	public class YobitRemoveOrderResult : IRemoveOrderResult
	{
		#region Public copnstructors



		public YobitRemoveOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region ICreateOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
