using System;
using System.Globalization;
using System.Xml.Linq;

namespace CultureWars.Extensions
{
	public static class XElementExtensions
	{
		private static XElement CreateNode(
			string elementName,
			string value)
		{
			return new XElement(elementName, value);
		}

		private static XElement CreateNode(
			string ns,
			string elementName,
			string value)
		{
			return new XElement($"{ns}:{elementName}", value);
		}

		private static XElement CreateNode(
			string elementName,
			XCData value)
		{
			return new XElement(elementName, value);
		}

		private static XElement CreateNode(
			string ns,
			string elementName,
			XCData value)
		{
			return new XElement($"{ns}:{elementName}", value);
		}

		public static XElement SubElement(
			this XElement @this,
			string elementName,
			string value)
		{
			@this.Add(CreateNode(elementName, value));
			return @this;
		}

		public static XElement SubElement(
			this XElement @this,
			string ns,
			string elementName,
			string value)
		{
			@this.Add(CreateNode(ns, elementName, value));
			return @this;
		}

		public static XElement SubElement(
			this XElement @this,
			string elementName,
			object value)
		{
			@this.Add(CreateNode(elementName, value.ToString()));
			return @this;
		}

		public static XElement SubElement(
			this XElement @this,
			string ns,
			string elementName,
			object value)
		{
			@this.Add(CreateNode(ns, elementName, value.ToString()));
			return @this;
		}

		public static XElement SubElement(
			this XElement @this,
			string elementName,
			XCData value)
		{
			@this.Add(CreateNode(elementName, value));
			return @this;
		}

		public static XElement SubElement(
			this XElement @this,
			string ns,
			string elementName,
			XCData value)
		{
			@this.Add(CreateNode(ns, elementName, value));
			return @this;
		}
		

		public static bool TryGetSubElement(
			this XElement @this,
			string elementName,
			out string value)
		{
			var firstResult = @this.Element(XName.Get(elementName));

			if (firstResult == null)
			{
				value = null;
				return false;
			}
			value = firstResult.Value;
			return true;
		}

		public static bool TryGetSubElement(
			this XElement @this,
			string ns,
			string elementName,
			out string value)
		{
			var firstResult = @this.Element(XName.Get(ns, elementName));

			if (firstResult == null)
			{
				value = null;
				return false;
			}
			value = firstResult.Value;
			return true;
		}


		public static bool TryGetIntSubElement(
			this XElement @this,
			string elementName,
			out int value)
		{
			var firstResult = @this.Element(XName.Get(elementName));
			if (firstResult == null)
			{
				value = default(int);
				return false;
			}
			if (!int.TryParse(firstResult.Value, out var integralValue))
			{
				value = default(int);
				return false;
			}
			value = integralValue;
			return true;
		}


		public static bool TryGetIntSubElement(
			this XElement @this,
			string ns,
			string elementName,
			out int value)
		{
			var firstResult = @this.Element(XName.Get(ns, elementName));
			if (firstResult == null)
			{
				value = default(int);
				return false;
			}
			if (!int.TryParse(firstResult.Value, out var integralValue))
			{
				value = default(int);
				return false;
			}
			value = integralValue;
			return true;
		}


		public static bool TryGetDateTimeSubElement(
			this XElement @this,
			string elementName,
			string dateTimeFormat,
			out DateTime value)
		{
			var firstResult = @this.Element(XName.Get(elementName));
			if (firstResult == null)
			{
				value = default(DateTime);
				return false;
			}
			if (!DateTime.TryParseExact(
				firstResult.Value,
				dateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var dateTimeValue))
			{
				value = default(DateTime);
				return false;
			}
			value = dateTimeValue;
			return true;
		}

		public static bool TryGetDateTimeSubElement(
			this XElement @this,
			string ns,
			string elementName,
			string dateTimeFormat,
			out DateTime value)
		{
			var firstResult = @this.Element(XName.Get(ns, elementName));
			if (firstResult == null)
			{
				value = default(DateTime);
				return false;
			}
			if (!DateTime.TryParseExact(
				firstResult.Value,
				dateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var dateTimeValue))
			{
				value = default(DateTime);
				return false;
			}
			value = dateTimeValue;
			return true;
		}
	}
}