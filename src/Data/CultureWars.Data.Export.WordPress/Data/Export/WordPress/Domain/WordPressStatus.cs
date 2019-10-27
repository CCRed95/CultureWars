using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	public partial struct WordPressStatus
	{
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