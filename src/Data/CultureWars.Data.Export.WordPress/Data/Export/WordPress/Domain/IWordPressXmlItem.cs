using System.Xml.Linq;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public interface IWordPressXmlItem
	{
		XElement ToXElement();
	}
}