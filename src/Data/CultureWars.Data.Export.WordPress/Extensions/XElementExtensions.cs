using Ccr.Std.Core.Extensions;
using CultureWars.Data.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using CultureWars.Data.Export.WordPress.Domain;

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

		public static string GetSubElement(
			this XElement @this,
			string elementName)
		{
			var firstResult = @this.Element(XName.Get(elementName));

			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{elementName}\" as {typeof(string).FormatName().SQuote()}.");
			}
			return firstResult.Value;
		}

		public static string GetSubElement(
			this XElement @this,
			string ns,
			string elementName)
		{
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{ns}:{elementName}\" as {typeof(string).FormatName().SQuote()}.");
			}
			return firstResult.Value;
		}


		public static int GetIntSubElement(
			this XElement @this,
			string elementName)
		{
			var firstResult = @this.Element(elementName);
			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{elementName}\" as {typeof(int).FormatName().SQuote()}.");
			}
			if (!int.TryParse(firstResult.Value, out var integralValue))
			{
				throw new XmlException(
					$"Could not find element \"{elementName}\" as {typeof(int).FormatName().SQuote()}.");
			}
			return integralValue;
		}


		public static int GetIntSubElement(
			this XElement @this,
			string ns,
			string elementName)
		{
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{ns}:{elementName}\" as {typeof(int).FormatName().SQuote()}.");
			}
			if (!int.TryParse(firstResult.Value, out var integralValue))
			{
				throw new XmlException(
					$"Could not find element \"{ns}:{elementName}\" as {typeof(int).FormatName().SQuote()}.");
			}
			return integralValue;
		}


		public static DateTime GetDateTimeSubElement(
			this XElement @this,
			string elementName,
			string dateTimeFormat)
		{
			var firstResult = @this.Element(XName.Get(elementName));
			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{elementName}\" as {nameof(DateTime).SQuote()}.");
			}
			if (!DateTime.TryParseExact(
				firstResult.Value,
				dateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var dateTimeValue))
			{
				throw new XmlException(
					$"Could not find element \"{elementName}\" as {nameof(DateTime).SQuote()}.");
			}
			return dateTimeValue;
		}

		public static DateTime GetDateTimeSubElement(
			this XElement @this,
			string ns,
			string elementName,
			string dateTimeFormat)
		{
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

			if (firstResult == null)
			{
				throw new XmlException(
					$"Could not find element \"{ns}:{elementName}\" as {nameof(DateTime).SQuote()}.");
			}
			if (!DateTime.TryParseExact(
				firstResult.Value,
				dateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out var dateTimeValue))
			{
				throw new XmlException(
					$"Could not find element \"{ns}:{elementName}\" as {nameof(DateTime).SQuote()}.");
			}
			return dateTimeValue;
		}


		public static int GetPostMetaThumbnailId(
			this XElement @this)
		{
			var wpNamespace = @this.GetNamespaceOfPrefix("wp");
			if (wpNamespace == null)
				throw new XmlException(
					$"Could not find the namespace definition \"wp\" in the XElement document.");

			var thumbnailPostMeta = @this.Element(
				XName.Get("postmeta", wpNamespace.NamespaceName));

			var postMetaKey = thumbnailPostMeta.GetSubElement("wp", "meta_key");
			if (postMetaKey != "_thumbnail_id")
				throw new XmlException(
					$"WordPress item's \"wp:postmeta.wp:meta_key\" value is {postMetaKey.Quote()}, and was " +
					$"expected to be \"_thumbnail_id\".");

			var thumbnailId = thumbnailPostMeta.GetIntSubElement("wp", "meta_value");
			return thumbnailId;
		}


		public static IEnumerable<WPCategory> GetPostCategories(
			this XElement @this)
		{
			var categoryNodes = @this.Elements(
				XName.Get("category"));

			foreach (var categoryNode in categoryNodes)
			{
				var domainAttribute = categoryNode.Attribute(XName.Get("domain"));
				if (domainAttribute == null)
					throw new XmlException(
						$"This \"category\" node has no \"domain\" attribute.");

				if (domainAttribute.Value != "category")
					continue;
				
				var nicenameAttribute = categoryNode.Attribute(XName.Get("nicename"));
				var categoryName = categoryNode.Value;
				var cultureWarsCategory = WPCategory.FromNameOrNull(categoryName);

				if (cultureWarsCategory == null)
				{
					if (nicenameAttribute == null)
						throw new XmlException(
							$"This \"category\" node has no \"nicename\" attribute.");

					cultureWarsCategory = new WPCategory(
						categoryName,
						nicenameAttribute.Value);
				}

				yield return cultureWarsCategory;
			}
		}

		public static IEnumerable<WPTerm> GetPostTags(
			this XElement @this)
		{
			var categoryNodes = @this.Elements(
				XName.Get("category"));

			foreach (var categoryNode in categoryNodes)
			{
				var domainAttribute = categoryNode.Attribute(XName.Get("domain"));
				if (domainAttribute == null)
					throw new XmlException(
						$"This \"category\" node has no \"domain\" attribute.");

				if (domainAttribute.Value != "post_tag")
					continue;

				var nicenameAttribute = categoryNode.Attribute(XName.Get("nicename"));
				var tagName = categoryNode.Value;
				var cultureWarsTag = WPTerm.FromNameOrNull(tagName);

				if (cultureWarsTag == null)
				{
					if (nicenameAttribute == null)
						throw new XmlException(
							$"This \"category\" node has no \"nicename\" attribute.");

					cultureWarsTag = new WPTerm(
						-1, // TODO fix
						tagName,
						nicenameAttribute.Value,
						tagName);
				}

				yield return cultureWarsTag;
			}
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
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

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
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

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
			var xmlns = @this.GetNamespaceOfPrefix(ns);
			if (xmlns == null)
				throw new XmlException(
					$"Could not find the namespace definition {ns.SQuote()} in the XElement document.");

			var firstResult = @this.Element(XName.Get(elementName, xmlns.NamespaceName));

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