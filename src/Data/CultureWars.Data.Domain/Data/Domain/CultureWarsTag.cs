using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CultureWars.Data.Domain
{
	public partial class CultureWarsTag
	{
		private static CultureWarsTag[] _cache;


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



		public CultureWarsTag(
			[NotNull] string tagFriendlyName)
		{
			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName));

			TagName = TagFriendlyName
				.Replace(".", "")
				.Replace("-", "")
				.Replace(".", "")
				.Replace(" ", "");

			HtmlEncodedTagName = TagFriendlyName
				.Replace(" ", "+");
		}

		public CultureWarsTag(
			[NotNull] string tagName,
			[NotNull] string tagFriendlyName,
			[NotNull] string htmlEncodedTagName)
		{
			TagName = tagName.EnforceNotNull(nameof(tagName));
			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName));
			HtmlEncodedTagName = htmlEncodedTagName.EnforceNotNull(nameof(htmlEncodedTagName));
		}



		public static CultureWarsTag[] AllTags
		{
			get
			{
				if (_cache == null)
				{
					var entities = typeof(CultureWarsTag)
						.GetFields(BindingFlags.Public | BindingFlags.Static)
						.Where(t => t.FieldType == typeof(CultureWarsTag))
						.Select(t => t.GetValue(null))
						.Cast<CultureWarsTag>()
						.ToArray();

					_cache = entities;
				}
				return _cache;
			}
		}

		public static CultureWarsTag FromName(
			string tagName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsTag)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsTag))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsTag>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagName == tagName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(CultureWarsTag).SQuote()} item with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsTag).SQuote()} item found with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagName == tagName);

			return result;
		}

		public static CultureWarsTag FromNameOrNull(
			string tagName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsTag)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsTag))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsTag>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagName == tagName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsTag).SQuote()} item found with the StatusName " +
					$"{tagName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagName == tagName);

			return result;
		}


		public static CultureWarsTag FromFriendlyName(
			string tagFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsTag)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsTag))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsTag>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagFriendlyName == tagFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(CultureWarsTag).SQuote()} item with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsTag).SQuote()} item found with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagFriendlyName == tagFriendlyName);

			return result;
		}

		public static CultureWarsTag FromFriendlyNameOrNull(
			string tagFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsTag)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsTag))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsTag>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.TagFriendlyName == tagFriendlyName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsTag).SQuote()} item found with the StatusName " +
					$"{tagFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.TagFriendlyName == tagFriendlyName);

			return result;
		}
	}
}