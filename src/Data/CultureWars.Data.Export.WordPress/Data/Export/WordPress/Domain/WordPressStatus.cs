using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial struct WordPressStatus
	{
		private static WordPressStatus[] _cache;


		/// <summary>
		///		Indicates the Status Name.
		/// </summary>
		[NotNull] public string StatusName { get; }

		/// <summary>
		///		Indicates the Status Nice/Friendly Name.
		/// </summary>
		[NotNull] public string StatusNiceName { get; }
		
		
		private WordPressStatus(
			[NotNull] string statusName,
			[NotNull] string statusNiceName)
		{
			StatusName = statusName.EnforceNotNull(nameof(statusName));
			StatusNiceName = statusNiceName.EnforceNotNull(nameof(statusNiceName));
		}


		public static WordPressStatus FromName(
			string statusName)
		{
			if (_cache == null)
			{
				var entities = typeof(WordPressStatus)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WordPressStatus))
					.Select(t => t.GetValue(null))
					.Cast<WordPressStatus>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.StatusName == statusName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WordPressStatus).SQuote()} item with the StatusName " +
					$"{statusName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WordPressStatus).SQuote()} item found with the StatusName " +
					$"{statusName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.StatusName == statusName);

			return result;
		}
		public static WordPressStatus FromFriendlyName(
			string statusFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(WordPressStatus)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WordPressStatus))
					.Select(t => t.GetValue(null))
					.Cast<WordPressStatus>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.StatusNiceName == statusFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WordPressStatus).SQuote()} item with the StatusName " +
					$"{statusFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WordPressStatus).SQuote()} item found with the StatusName " +
					$"{statusFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.StatusName == statusFriendlyName);

			return result;
		}
	}
	
	public partial struct WordPressStatus
	{
		/// <summary>
		///		Publish Status Entity Declaration
		/// </summary>
		public static readonly WordPressStatus Publish
			= new WordPressStatus("publish", "Publish");

		/// <summary>
		///		Private Status Entity Declaration
		/// </summary>
		public static readonly WordPressStatus Private
			= new WordPressStatus("private", "Private");

		/// <summary>
		///		Future Status Entity Declaration
		/// </summary>
		public static readonly WordPressStatus Future
			= new WordPressStatus("future", "Future");

		/// <summary>
		///		Draft Status Entity Declaration
		/// </summary>
		public static readonly WordPressStatus Draft
			= new WordPressStatus("draft", "Draft");

		/// <summary>
		///		Pending Status Entity Declaration
		/// </summary>
		public static readonly WordPressStatus Pending
			= new WordPressStatus("pending", "Pending");
	}
}