using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXDeclaration
	{
		public double XmlVersion { get; }

		public string Encoding { get; }

		public bool IsStandalone { get; }


		private FXDeclaration(
			double xmlVersion,
			[NotNull] string encoding,
			bool isStandalone = true)
		{
			XmlVersion = xmlVersion;
			Encoding = encoding.EnforceNotNull(nameof(encoding));
			IsStandalone = isStandalone;
		}


		public static FXDeclaration Get(
			double xmlVersion,
			[NotNull] string encoding,
			bool isStandalone = true)
		{
			return new FXDeclaration(
				xmlVersion,
				encoding,
				isStandalone);
		}

		//public void WriteXmlDeclaration()
		//{
		//	_writer.WriteLine($"<?xml version=\"{_xmlVersion}\" encoding=\"utf-8\" standalone=\"yes\"?>");
		//}
	}
}