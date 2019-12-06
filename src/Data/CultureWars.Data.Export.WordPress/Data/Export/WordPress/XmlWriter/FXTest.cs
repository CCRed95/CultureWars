namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public static class FXTest
	{
		private static readonly FXNamespace excerptNs = ("excerpt", "http://wordpress.org/export/1.2/excerpt/");
		private static readonly FXNamespace contentNs = ("content", "http://purl.org/rss/1.0/modules/content/");
		private static readonly FXNamespace wfwNs = ("wfw", "http://wellformedweb.org/CommentAPI/");
		private static readonly FXNamespace dcNs = ("dc", "http://purl.org/dc/elements/1.1/");
		private static readonly FXNamespace wpNs = ("wp", "http://wordpress.org/export/1.2/");

		private const double _xmlVersion = 1.0;
		private const double _wxrVersion = 1.2;


		public static void FXDocumentTest()
		{
			var document = FXDocument.Declare("rss")
			                         .WithDeclaration(
				                         FXDeclaration.Get(_xmlVersion, "utf-8"))
			                         .WithNamespace(excerptNs)
			                         .WithNamespace(contentNs)
			                         .WithNamespace(wfwNs)
			                         .WithNamespace(dcNs)
			                         .WithNamespace(wpNs);
			document
				.CreateSubNode(
					t => t.AddNode(
						FXElement.Builder.WithSimpleChildNode("wp:wxr_version", _wxrVersion)));

			document
				.CreateSubNode(
					t => t.AddNode(
						FXElement.Builder
							.WithComplexChildNode("wp:tag", 
								n => n.WithSimpleChildNode("wp:tag_id", 0)
								      .WithSimpleChildNode("wp:tag_slug", FXCData.Get("Apparition"))
								      .WithSimpleChildNode("wp:tag_name", FXCData.Get("Apparition")))));

			//document
			//	.CreateSubNode(t => t.AddNode(FXElement.Get("wp:tag_id", 1)))
			//	.CreateSubNode(t => t.AddNode(FXElement.Get("wp:tag_slug", FXCData.Get("slug-term"))))
			//	.CreateSubNode(t => t.AddNode(FXElement.Get("wp:tag_name", FXCData.Get("Tag Name"))));
		}
	}
}