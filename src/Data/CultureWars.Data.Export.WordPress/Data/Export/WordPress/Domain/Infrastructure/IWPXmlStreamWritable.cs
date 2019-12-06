namespace CultureWars.Data.Export.WordPress.Domain.Infrastructure
{
	public interface IWPXmlStreamWritable
	{
		void WriteToXmlStream(XmlStreamWriter writer);
	}
}