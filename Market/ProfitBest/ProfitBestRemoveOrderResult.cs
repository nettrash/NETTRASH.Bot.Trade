using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Market.ProfitBest
{
	public class ProfitBestRemoveOrderResult : IRemoveOrderResult
	{
		#region Public copnstructors



		public ProfitBestRemoveOrderResult(bool bSuccess)
		{
			Success = bSuccess;
		}



		#endregion
		#region IRemoveOrderResult members



		public bool Success { get; private set; }



		#endregion
	}
}
