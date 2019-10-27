using System;
using System.Collections.Generic;
using System.Xml;
using CultureWars.Data.Domain;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public class WordPressXmlWriter
		: WordPressXmlWriterBase
	{
		public WordPressXmlWriter(
			[NotNull] XmlTextWriter xmlTextWriter)
			: base(xmlTextWriter)
		{
		}


		public WordPressItemXmlWriter BeginWriteItem()
		{
			_xmlTextWriter.WriteStartElement("item");
			return new WordPressItemXmlWriter(this);
		}
	}


	public class WordPressChannelDocument
	{
		public string ChannelTitle { get; }

		public string ChannelLink { get; }

		public DateTime PublicationDate { get; }

		public string ChannelDescription { get; }

		public string LanguageCode { get; }

		public double WXRVersion { get; }

		public IReadOnlyList<CultureWarsAuthor> Authors { get; }

		public IReadOnlyList<CultureWarsCategory> Categories { get; }

		public IReadOnlyList<WordPressItem>


	}

}