using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial struct WordPressCommentStatus
	{
		/// <summary>
		///		Indicates the Status Name.
		/// </summary>
		[NotNull] public string StatusName { get; }

		/// <summary>
		///		Indicates the Status Nice/Friendly Name.
		/// </summary>
		[NotNull] public string StatusNiceName { get; }


		private WordPressCommentStatus(
			[NotNull] string metaTypeName,
			[NotNull] string metaTypeNiceName)
		{
			StatusName = metaTypeName.EnforceNotNull(nameof(metaTypeName));
			StatusNiceName = metaTypeNiceName.EnforceNotNull(nameof(metaTypeNiceName));
		}
	}


	public partial struct WordPressCommentStatus
	{
		/// <summary>
		///		Pending Status Entity Declaration
		/// </summary>
		public static readonly WordPressCommentStatus Pending
			= new WordPressCommentStatus("pending", "Pending");

		/// <summary>
		///		Approved Status Entity Declaration
		/// </summary>
		public static readonly WordPressCommentStatus Approved
			= new WordPressCommentStatus("approved", "Approved");

		/// <summary>
		///		Spam Status Entity Declaration
		/// </summary>
		public static readonly WordPressCommentStatus Spam
			= new WordPressCommentStatus("spam", "Spam");

		/// <summary>
		///		Trash Status Entity Declaration
		/// </summary>
		public static readonly WordPressCommentStatus Trash
			= new WordPressCommentStatus("trash", "Trash");
	}
}