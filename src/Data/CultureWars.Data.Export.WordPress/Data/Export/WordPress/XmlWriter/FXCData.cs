using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXCData
	{
		[CanBeNull]
		public string Value { get; }


		private FXCData(
			[CanBeNull] string value)
		{
			Value = value;
		}


		public static FXCData Get(
			[CanBeNull] string value)
		{
			return new FXCData(value);
		}


		public static implicit operator FXCData(
			string value)
		{
			return Get(value);
		}
	}
}