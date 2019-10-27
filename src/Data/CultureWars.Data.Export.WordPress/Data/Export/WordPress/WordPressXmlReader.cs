﻿using System;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public class WordPressXmlReader
		: WordPressXmlReaderBase
	{
		public WordPressXmlReader(
			[NotNull] XDocument document)
			: base(document)
		{
		}

		public WordPressXmlReader BeginWriteItem()
		{
			throw new NotImplementedException();
			//_xmlTextWriter.WriteStartElement("item");
			//return new WordPressItemXmlWriter(this);
		}
	}
}