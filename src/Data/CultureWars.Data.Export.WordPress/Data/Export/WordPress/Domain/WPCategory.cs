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
	public partial class WPCategory
		: IWPXmlStreamWritable
	{
		private static WPCategory[] _cache;


		public int CultureWarsCategoryID { get; set; }

		/// <summary>
		///		Indicates the Category Name.
		/// </summary>
		[NotNull] public string CategoryName { get; set; }

		/// <summary>
		///		Indicates the Category Nice/Friendly Name.
		/// </summary>
		[NotNull] public string CategoryNiceName { get; set; }

		/// <summary>
		///		Indicates the Category Name's parent name, if applicable.
		/// </summary>
		[CanBeNull] public string CategoryParent { get; set; }



		public WPCategory(
			[NotNull] string categoryName,
			[NotNull] string categoryNiceName,
			[CanBeNull] string categoryParent = null)
		{
			CategoryName = categoryName.EnforceNotNull(nameof(categoryName));
			CategoryNiceName = categoryNiceName.EnforceNotNull(nameof(categoryNiceName));
			CategoryParent = categoryParent;
		}


		/// <inheritdoc/>
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var wpNs = FXNsRef.FromName("wp");

			writer.WriteStartElement(wpNs, "category")
			      .WriteInlineCDataElement(wpNs, "cat_name", CategoryName)
			      .WriteInlineCDataElement(wpNs, "category_nicename", CategoryNiceName)
			      .WriteInlineElement(wpNs, "category_parent", "0")
			      .WriteEndElement();
		}



		public static WPCategory[] AllCategories
		{
			get
			{
				if (_cache == null)
				{
					var entities = typeof(WPCategory)
					               .GetFields(BindingFlags.Public | BindingFlags.Static)
					               .Where(t => t.FieldType == typeof(WPCategory))
					               .Select(t => t.GetValue(null))
					               .Cast<WPCategory>()
					               .ToArray();

					_cache = entities;
				}
				return _cache;
			}
		}

		public static WPCategory FromName(
			string categoryName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPCategory))
					.Select(t => t.GetValue(null))
					.Cast<WPCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryName == categoryName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPCategory).SQuote()} item with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPCategory).SQuote()} item found with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryName == categoryName);

			return result;
		}

		public static WPCategory FromNameOrNull(
			string categoryName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPCategory))
					.Select(t => t.GetValue(null))
					.Cast<WPCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryName == categoryName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPCategory).SQuote()} item found with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryName == categoryName);

			return result;
		}

		public static WPCategory FromFriendlyName(
			string categoryFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPCategory))
					.Select(t => t.GetValue(null))
					.Cast<WPCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryNiceName == categoryFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPCategory).SQuote()} item with the StatusName " +
					$"{categoryFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPCategory).SQuote()} item found with the StatusName " +
					$"{categoryFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryNiceName == categoryFriendlyName);

			return result;
		}

	}
}