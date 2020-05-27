using System.Collections.Generic;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXSubNodeCreator
	{
		private readonly FXDocument _fxDocument;
		private readonly List<FXElement> _fxElements
			= new List<FXElement>();


		public FXSubNodeCreator(
			FXDocument fxDocument)
		{
			_fxDocument = fxDocument.EnforceNotNull(nameof(fxDocument));
		}


		public FXSubNodeCreator AddNode(
			FXElement fxElement)
		{
			_fxElements.Add(fxElement);
			return this;
		}
	}
}