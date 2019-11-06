using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public partial class CultureWarsCategory
	{
		private static CultureWarsCategory[] _cache;


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



		public CultureWarsCategory(
			[NotNull] string categoryName,
			[NotNull] string categoryNiceName,
			[CanBeNull] string categoryParent = null)
		{
			CategoryName = categoryName.EnforceNotNull(nameof(categoryName));
			CategoryNiceName = categoryNiceName.EnforceNotNull(nameof(categoryNiceName));
			CategoryParent = categoryParent;
		}


		public static CultureWarsCategory FromName(
			string categoryName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsCategory))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryName == categoryName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(CultureWarsCategory).SQuote()} item with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsCategory).SQuote()} item found with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryName == categoryName);

			return result;
		}

		public static CultureWarsCategory FromNameOrNull(
			string categoryName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsCategory))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryName == categoryName);

			if (resultCount == 0)
				return null;

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsCategory).SQuote()} item found with the StatusName " +
					$"{categoryName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryName == categoryName);

			return result;
		}


		public static CultureWarsCategory FromFriendlyName(
			string categoryFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(CultureWarsCategory)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(CultureWarsCategory))
					.Select(t => t.GetValue(null))
					.Cast<CultureWarsCategory>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.CategoryNiceName == categoryFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(CultureWarsCategory).SQuote()} item with the StatusName " +
					$"{categoryFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(CultureWarsCategory).SQuote()} item found with the StatusName " +
					$"{categoryFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.CategoryNiceName == categoryFriendlyName);

			return result;
		}
	}
}