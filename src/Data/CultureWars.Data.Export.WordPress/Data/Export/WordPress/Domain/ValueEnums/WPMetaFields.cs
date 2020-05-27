using System;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain.ValueEnums
{
	public partial struct WordPressMetaType
	{
		/// <summary>
		///		Indicates the MetaType Name.
		/// </summary>
		[NotNull] public string MetaTypeName { get; }

		/// <summary>
		///		Indicates the MetaType Nice/Friendly Name.
		/// </summary>
		[NotNull] public string MetaTypeNiceName { get; }


		private WordPressMetaType(
			[NotNull] string metaTypeName,
			[NotNull] string metaTypeNiceName)
		{
			MetaTypeName = metaTypeName.EnforceNotNull(nameof(metaTypeName));
			MetaTypeNiceName = metaTypeNiceName.EnforceNotNull(nameof(metaTypeNiceName));
		}
	}
	

	public partial struct WordPressMetaType
	{
		/// <summary>
		///		Post MetaType Entity Declaration
		/// </summary>
		public static readonly WordPressMetaType Post
			= new WordPressMetaType("post", "Post");

		/// <summary>
		///		Post MetaType Entity Declaration
		/// </summary>
		public static readonly WordPressMetaType Comment
			= new WordPressMetaType("comment", "Comment");

		/// <summary>
		///		Term MetaType Entity Declaration
		/// </summary>
		public static readonly WordPressMetaType Term
			= new WordPressMetaType("term", "Term");

		/// <summary>
		///		User MetaType Entity Declaration
		/// </summary>
		public static readonly WordPressMetaType User
			= new WordPressMetaType("user", "User");

		/// <summary>
		///		CUSTOM MetaType Entity Declaration
		/// </summary>
		[Obsolete]
		public static readonly WordPressMetaType CUSTOM
			= new WordPressMetaType("custom", "CUSTOM");
	}
}
