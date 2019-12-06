using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.ValueEnums
{
	public partial struct WPCommentStatus
	{
		/// <summary>
		///		Indicates the Status Name.
		/// </summary>
		[NotNull] public string StatusName { get; }

		/// <summary>
		///		Indicates the Status Nice/Friendly Name.
		/// </summary>
		[NotNull] public string StatusNiceName { get; }


		private WPCommentStatus(
			[NotNull] string metaTypeName,
			[NotNull] string metaTypeNiceName)
		{
			StatusName = metaTypeName.EnforceNotNull(nameof(metaTypeName));
			StatusNiceName = metaTypeNiceName.EnforceNotNull(nameof(metaTypeNiceName));
		}
	}


	public partial struct WPCommentStatus
	{
		/// <summary>
		///		Pending Status Entity Declaration
		/// </summary>
		public static readonly WPCommentStatus Pending
			= new WPCommentStatus("pending", "Pending");

		/// <summary>
		///		Approved Status Entity Declaration
		/// </summary>
		public static readonly WPCommentStatus Approved
			= new WPCommentStatus("approved", "Approved");

		/// <summary>
		///		Spam Status Entity Declaration
		/// </summary>
		public static readonly WPCommentStatus Spam
			= new WPCommentStatus("spam", "Spam");

		/// <summary>
		///		Trash Status Entity Declaration
		/// </summary>
		public static readonly WPCommentStatus Trash
			= new WPCommentStatus("trash", "Trash");
	}
}