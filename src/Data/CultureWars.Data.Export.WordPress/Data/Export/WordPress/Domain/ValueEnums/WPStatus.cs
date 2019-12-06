using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.ValueEnums
{
	public partial struct WPStatus
	{
		private static WPStatus[] _cache;


		/// <summary>
		///		Indicates the Status Name.
		/// </summary>
		[NotNull] public string StatusName { get; }

		/// <summary>
		///		Indicates the Status Nice/Friendly Name.
		/// </summary>
		[NotNull] public string StatusNiceName { get; }
		
		
		private WPStatus(
			[NotNull] string statusName,
			[NotNull] string statusNiceName)
		{
			StatusName = statusName.EnforceNotNull(nameof(statusName));
			StatusNiceName = statusNiceName.EnforceNotNull(nameof(statusNiceName));
		}


		public static WPStatus FromName(
			string statusName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPStatus)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPStatus))
					.Select(t => t.GetValue(null))
					.Cast<WPStatus>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.StatusName == statusName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPStatus).SQuote()} item with the StatusName " +
					$"{statusName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPStatus).SQuote()} item found with the StatusName " +
					$"{statusName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.StatusName == statusName);

			return result;
		}
		public static WPStatus FromFriendlyName(
			string statusFriendlyName)
		{
			if (_cache == null)
			{
				var entities = typeof(WPStatus)
					.GetFields(BindingFlags.Public | BindingFlags.Static)
					.Where(t => t.FieldType == typeof(WPStatus))
					.Select(t => t.GetValue(null))
					.Cast<WPStatus>()
					.ToArray();

				_cache = entities;
			}

			var resultCount = _cache.Count(
				t => t.StatusNiceName == statusFriendlyName);

			if (resultCount == 0)
				throw new KeyNotFoundException(
					$"Could not find {nameof(WPStatus).SQuote()} item with the StatusName " +
					$"{statusFriendlyName.Quote()} defined as a public static field.");

			if (resultCount > 1)
				throw new KeyNotFoundException(
					$"More than one {nameof(WPStatus).SQuote()} item found with the StatusName " +
					$"{statusFriendlyName.Quote()} defined as a public static field.");

			var result = _cache.Single(
				t => t.StatusName == statusFriendlyName);

			return result;
		}
	}
	
	public partial struct WPStatus
	{
		/// <summary>
		///		Publish Status Entity Declaration
		/// </summary>
		public static readonly WPStatus Publish
			= new WPStatus("publish", "Publish");

		/// <summary>
		///		Private Status Entity Declaration
		/// </summary>
		public static readonly WPStatus Private
			= new WPStatus("private", "Private");

		/// <summary>
		///		Future Status Entity Declaration
		/// </summary>
		public static readonly WPStatus Future
			= new WPStatus("future", "Future");

		/// <summary>
		///		Draft Status Entity Declaration
		/// </summary>
		public static readonly WPStatus Draft
			= new WPStatus("draft", "Draft");

		/// <summary>
		///		Pending Status Entity Declaration
		/// </summary>
		public static readonly WPStatus Pending
			= new WPStatus("pending", "Pending");
	}
}