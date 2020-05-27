using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.ValueEnums
{
	/// <summary>
	///		Status of Comments
	/// </summary>
	public partial struct WPOpenStatus
	{
		/// <summary>
		///		Indicates the Status ID.
		/// </summary>
		public int StatusID { get; }

		/// <summary>
		///		Indicates the Status Name.
		/// </summary>
		[NotNull]
		public string StatusName { get; }

		/// <summary>
		///		Indicates the Status Nice/Friendly Name.
		/// </summary>
		[NotNull]
		public string StatusFriendlyName { get; }

		
		private WPOpenStatus(
			int statusID,
			[NotNull] string statusName,
			[NotNull] string statusFriendlyName)
		{
			StatusID = statusID;
			StatusName = statusName.EnforceNotNull(nameof(statusName));
			StatusFriendlyName = statusFriendlyName.EnforceNotNull(nameof(statusFriendlyName));
		}
	}


	public partial struct WPOpenStatus
	{
		/// <summary>
		///		Open WordPressOpenStatus Entity Declaration
		/// </summary>
		public static readonly WPOpenStatus Open
			= new WPOpenStatus(1, "open", "Open");

		/// <summary>
		///		Closed WordPressOpenStatus Entity Declaration
		/// </summary>
		public static readonly WPOpenStatus Closed
			= new WPOpenStatus(2, "closed", "Closed");
	}
}