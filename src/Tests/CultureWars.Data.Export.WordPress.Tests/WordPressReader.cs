using CultureWars.Data.Domain;
using CultureWars.Data.Export.WordPress.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using CultureWars.Extensions;

namespace CultureWars.Data.Export.WordPress.Tests
{
	[TestFixture]
	public class WordPressReader
	{
		[Test]
		[TestCaseSource(nameof(ReadXmlFile), new object[] { "Comment.xml" })]
		public void CanReadPostComment(
			XDocument commentNode)
		{
			//var comment = WPPostComment.FromXElement(commentNode.Root);
		}

		[Test]
		[TestCaseSource(nameof(ReadXmlFile), new object[] { "WordPressPostItem.xml" })]
		public void CanReadPost(
			XDocument postNode)
		{
			//var post = WPPostItem.FromXElement(postNode.Root);
		}
		//<iframe src = "//www.youtube.com/embed/NBD4c6fkdQE?wmode=opaque&amp;enablejsapi=1" height="480" width="854" scrolling="no" frameborder="0" allowfullscreen="">
		//</iframe>

		[Test]
		public void CanWritePosts()
		{


		}

		[Test]
		public void CanWriteTags()
		{
			var xDocument = new XDocument(
				new XDeclaration("1.0", "UTF-8", "yes"));

			var rssNode = new XElement(XName.Get("rss"));
			xDocument.Add(rssNode);

			var channelNode = new XElement(XName.Get("channel"));
			rssNode.Add(channelNode);

			var wxrVersionName = XName.Get(
				"wxr_version",
				"http://wordpress.org/export/1.2/");

			var wxrVersionNode = new XElement(wxrVersionName)
			{
				Value = "1.2"
			};

			channelNode.Add(wxrVersionNode);

			var index = 0;

			foreach (var tag in WPTerm.AllTags)
			{
				var categoryNode = new XElement(
					XName.Get("tag", "http://wordpress.org/export/1.2/"));

				var termIdNode = new XElement(
					XName.Get("term_id", "http://wordpress.org/export/1.2/"))
				{
					Value = index.ToString()
				};

				var tagSlugNode = new XElement(
					XName.Get("tag_slug", "http://wordpress.org/export/1.2/"));

				tagSlugNode.Add(new XCData(tag.HtmlEncodedTagName));

				var tagNameNode = new XElement(
					XName.Get("tag_name", "http://wordpress.org/export/1.2/"));

				tagNameNode.Add(new XCData(tag.TagFriendlyName));

				categoryNode.Add(termIdNode);
				categoryNode.Add(tagSlugNode);
				categoryNode.Add(tagNameNode);

				channelNode.Add(categoryNode);

				index++;
			}

			var desktopDirectory = new DirectoryInfo(
				Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

			var exportFile = new FileInfo(
				desktopDirectory.FullName + @"\CultureWarsTags.xml");

			using var textWriter = exportFile.CreateText();
			using var xmlWriter = System.Xml.XmlWriter.Create(textWriter);

			xDocument.Save(xmlWriter);
		}


		private static IEnumerable<XDocument> ReadXmlFile(
			string fileName)
		{
			var workingDirectory = new DirectoryInfo(
				Environment.CurrentDirectory);

			var projectDirectory = workingDirectory.Parent?.Parent?.Parent;
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
					var xmlStr = streamReader.ReadToEnd();
					yield return XDocument.Parse(xmlStr);
				}
			}
		}

	}
}
