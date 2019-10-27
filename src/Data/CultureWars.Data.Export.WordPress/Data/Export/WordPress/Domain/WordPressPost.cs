using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	/// <summary>
	/// Status of Comments
	/// </summary>
	public partial struct OpenStatus
	{
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


		private OpenStatus(
			[NotNull] string statusName,
			[NotNull] string statusFriendlyName)
		{
			StatusName = statusName.EnforceNotNull(nameof(statusName));
			StatusFriendlyName = statusFriendlyName.EnforceNotNull(nameof(statusFriendlyName));
		}
	}

	public partial struct OpenStatus
	{
		/// <summary>
		///		Open OpenStatus Entity Declaration
		/// </summary>
		public static readonly OpenStatus Open
			= new OpenStatus("open", "Open");

		/// <summary>
		///		Closed OpenStatus Entity Declaration
		/// </summary>
		public static readonly OpenStatus Closed
			= new OpenStatus("closed", "Closed");
	}
}