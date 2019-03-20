using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace NETTRASH.BOT.Trade.Bot.Configuration
{
	[Serializable]
	[XmlRoot(ElementName = "bot_config")]
	public class BaseBotConfiguration
	{
		#region Public properties



		[XmlElement(ElementName = "version")]
		public int Version { get; set; }



		#endregion
		#region Public methods



		public async Task<string> SerializeAsync()
		{
			XmlSerializer ser = new XmlSerializer(GetType());
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = false;
			settings.Encoding = Encoding.UTF8;
			settings.OmitXmlDeclaration = true;
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			System.IO.MemoryStream ms = new System.IO.MemoryStream();
			XmlWriter writer = XmlWriter.Create(ms, settings);

			ser.Serialize(writer, this, ns);
			await writer.FlushAsync();

			byte[] data = new byte[ms.Length];
			ms.Position = 0;
			await ms.ReadAsync(data, 0, data.Length);
			return Encoding.UTF8.GetString(data);
		}



		#endregion
	}
}
