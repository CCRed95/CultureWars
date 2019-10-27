using System.Xml;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public abstract class WordPressXmlWriterBase
	{
		protected readonly XmlTextWriter _xmlTextWriter;
		

		protected WordPressXmlWriterBase(
			[NotNull] XmlTextWriter xmlTextWriter)
		{
			_xmlTextWriter = xmlTextWriter.EnforceNotNull(nameof(xmlTextWriter));
		}


		public static implicit operator XmlTextWriter(
			WordPressXmlWriterBase @this)
		{
			return @this._xmlTextWriter;
		}
	}


	//public class WordPressChannel
	//{
	//	public string ChannelTitle { get; }

	//	public string ChannelLink { get; }

	//	public DateTime? PublishedDate { get; }

	//	public string ChannelDescription { get; }

	//	public string LanguageCode { get; }

	//	public double? WXRVersion { get; }

	//	public IReadOnlyList<CultureWarsAuthor> Authors { get; }

	//	//public IReadOnlyList<CultureWarsAuthor> Authors { get; }



	//}


}
