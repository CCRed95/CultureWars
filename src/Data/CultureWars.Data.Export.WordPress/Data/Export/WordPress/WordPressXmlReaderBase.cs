using System.Xml.Linq;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public abstract class WordPressXmlReaderBase
	{
		protected readonly XDocument _document;


		protected WordPressXmlReaderBase(
			[NotNull] XDocument document)
		{
			_document = document.EnforceNotNull(nameof(document));
		}


		//public static implicit operator XmlTextWriter(
		//	WordPressXmlWriterBase @this)
		//{
		//	return @this._xmlTextWriter;
		//}
	}
}