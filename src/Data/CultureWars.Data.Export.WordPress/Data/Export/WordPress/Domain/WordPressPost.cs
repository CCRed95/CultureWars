using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	/// <summary>
	///		Status of Comments
	/// </summary>
	public partial struct WordPressOpenStatus
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

		
		private WordPressOpenStatus(
			int statusID,
			[NotNull] string statusName,
			[NotNull] string statusFriendlyName)
		{
			StatusID = statusID;
			StatusName = statusName.EnforceNotNull(nameof(statusName));
			StatusFriendlyName = statusFriendlyName.EnforceNotNull(nameof(statusFriendlyName));
		}
	}


	public partial struct WordPressOpenStatus
	{
		/// <summary>
		///		Open WordPressOpenStatus Entity Declaration
		/// </summary>
		public static readonly WordPressOpenStatus Open
			= new WordPressOpenStatus(1, "open", "Open");

		/// <summary>
		///		Closed WordPressOpenStatus Entity Declaration
		/// </summary>
		public static readonly WordPressOpenStatus Closed
			= new WordPressOpenStatus(2, "closed", "Closed");
	}
}