using System;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;

namespace CultureWars.Data.Export.WordPress.Tests
{
	public class WordPressReader
	{
		[TestCaseSource(nameof(ReadXmlFile), new object[] { "Comment.xml" })]
		public void CanReadPostComment(
			string xmlText)
		{
			var comment = XDocument.Load(xmlText);
		}
		[TestCaseSource(nameof(ReadXmlFile), new object[] { "WordPressPostItem.xml" })]
		public void CanReadPost(
			string xmlText)
		{
			var comment = XDocument.Load(xmlText);
		}



		private static string ReadXmlFile(
			string fileName)
		{
			var workingDirectory = new DirectoryInfo(
				Environment.CurrentDirectory);

			var projectDirectory = workingDirectory.Parent?.Parent;
			if (projectDirectory == null)
				throw new FileNotFoundException(
					$"File not found.");

			var xmlFiles = new DirectoryInfo(
				projectDirectory.FullName + @"\XmlFiles\");

			var xmlFile = new FileInfo(xmlFiles.FullName + fileName);

			using (var reader = xmlFile.OpenRead())
			{
				using (var streamReader = new StreamReader(reader))
				{
					return streamReader.ReadToEnd();
				}
			}
		}
	}
}
