using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.XmlWriter;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial class WPTerm
		: IWPXmlStreamWritable
	{
		private static WPTerm[] _cache;


		public int WPTermID { get; set; }

		/// <summary>
		///		Indicates the Category Name.
		/// </summary>
		[NotNull]
		public string TagName { get; set; }

		/// <summary>
		///		Indicates the Category Nice/Friendly Name.
		/// </summary>
		[NotNull]
		public string TagFriendlyName { get; set; }

		/// <summary>
		///		Indicates the Category Name's parent name, if applicable.
		/// </summary>
		[NotNull]
		public string HtmlEncodedTagName { get; set; }



		public WPTerm(
			int wpTermID,
			[NotNull] string tagFriendlyName)
		{
			WPTermID = wpTermID;

			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName))
			                                 .ToTitleCase();

			TagName = TagFriendlyName
				 .Replace(".", "")
				 .Replace("-", "_")
				 .Replace(".", "")
				 .Replace(" ", "_")
				 .Replace("\"", "")
				 .Replace("'", "");

			if (!TagName.IsValidCSharpIdentifier())
			{

			}

			HtmlEncodedTagName = TagFriendlyName
				.Replace(" ", "+");
		}

		public WPTerm(
			int wpTermID,
			[NotNull] string tagName,
			[NotNull] string tagFriendlyName,
			[NotNull] string htmlEncodedTagName)
		{
			WPTermID = wpTermID;
			TagName = tagName.EnforceNotNull(nameof(tagName));
			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName));
			HtmlEncodedTagName = htmlEncodedTagName.EnforceNotNull(nameof(htmlEncodedTagName));
		}



		public static WPTerm[] AllTags
		{
			get
			{
				if (_cache == null)
				{
					var entities = typeof(WPTerm)
						.GetFields(BindingFlags.Public | BindingFlags.Static)
						.Where(t => t.FieldType == typeof(WPTerm))
						.Select(t => t.GetValue(null))
						.Cast<WPTerm>()
						.ToArray();

					_cache = entities;
				}
				return _cache;
			}
		}
		

		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var wpNs = FXNsRef.FromName("wp");

			writer.WriteStartElement(wpNs, "tag")
			      .WriteInlineElement(wpNs, "term_id", WPTermID.ToString())
			      .WriteInlineCDataElement(wpNs, "tag_slug", HtmlEncodedTagName)
			      .WriteInlineCDataElement(wpNs, "tag_name", TagFriendlyName)
			      .WriteEndElement();
		}


		public static WPTerm FromName(
			string tagName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPTerm)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPTerm))
					.Select(t => t.GetValue(null))
					.Cast<WPTerm>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagName == tagName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPTerm).SQuote()} item with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPTerm).SQuote()} item found with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagName == tagName);

			return result;
		}

		public static WPTerm FromNameOrNull(
			string tagName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPTerm)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPTerm))
					.Select(t => t.GetValue(null))
					.Cast<WPTerm>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagName == tagName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPTerm).SQuote()} item found with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagName == tagName);

			return result;
		}


		public static WPTerm FromFriendlyName(
			string tagFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPTerm)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPTerm))
					.Select(t => t.GetValue(null))
					.Cast<WPTerm>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagFriendlyName == tagFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPTerm).SQuote()} item with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPTerm).SQuote()} item found with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagFriendlyName == tagFriendlyName);

			return result;
		}

		public static WPTerm FromFriendlyNameOrNull(
			string tagFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPTerm)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPTerm))
					.Select(t => t.GetValue(null))
					.Cast<WPTerm>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagFriendlyName == tagFriendlyName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPTerm).SQuote()} item found with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagFriendlyName == tagFriendlyName);

			return result;
		}
	}
}