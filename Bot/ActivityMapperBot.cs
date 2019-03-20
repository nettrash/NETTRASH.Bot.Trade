using NETTRASH.BOT.Trade.Market;

namespace NETTRASH.BOT.Trade.Bot
{
	public class ActivityMapperBot : BaseBot
	{
		#region Public properties



		public MarketType Source { get; }

		public MarketType Target { get; }



		#endregion
		#region Public constructors



		public ActivityMapperBot(MarketType source, MarketType target)
			: base(BotType.ActivityMapper)
		{
			Source = source;
			Target = target;
		}

		public ActivityMapperBot(int nId)
			: base(nId)
		{
			if (TypeOfBot != BotType.ActivityMapper)
				throw new System.NotSupportedException($"Can't initialize ActivityMapper with different type {TypeOfBot}");
			_Initialize();
		}




		#endregion
		#region Private methods



		private void _Initialize()
		{

		}



		#endregion
		#region Public methods



		public void Map()
		{
			//Грузим ордера из маркета источника
			//Ищем новые ордера относительно маркета получателя
			//Ставим новые ордера
			//Ищем завершенные ордера и переставляем ордера для завершения 
		}



		#endregion
	}
}
