using System.IO;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FluentXmlWriter
	{
		protected readonly StreamWriter _writer;


		public FluentXmlWriter(
			[NotNull] StreamWriter streamWriter)
		{
			_writer = streamWriter.EnforceNotNull(nameof(streamWriter));
		}





		//public FluentXmlWriter WriteSimpleNode(
		//	FXElement fxElement)
		//{

		//}



	}
}
