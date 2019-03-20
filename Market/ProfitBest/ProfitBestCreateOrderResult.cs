using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestCreateOrderResult : ICreateOrderResult
	{
		#region Public copnstructors



		public ProfitBestCreateOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region ICreateOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
