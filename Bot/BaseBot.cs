using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETTRASH.BOT.Trade.Bot
{
	public class BaseBot
	{
		#region Public properties



		public BotType TypeOfBot { get; private set; }



		#endregion
		#region Public constructors



		protected BaseBot(BotType type)
		{
			TypeOfBot = type;
		}

		protected BaseBot(int nId)
		{
			_LoadBot(nId);
		}



		#endregion
		#region Private methods



		private void _LoadBot(int nId)
		{

		}



		#endregion
	}
}
