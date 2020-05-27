using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.Domain.Infrastructure;
using CultureWars.Data.Export.WordPress.XmlWriter;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.Domain
{
	/// <summary>
	///		The type declaration for the <see cref="WPAuthor"/> entity.
	/// </summary>
	public partial class WPAuthor
		: IWPXmlStreamWritable
	{
		/// <summary>
		///		Indicates the ID of the author.
		/// </summary>
		public int AuthorID { get; set; }

		/// <summary>
		///		Indicates the login of the author.
		/// </summary>
		[CanBeNull] public string Login { get; set; }

		/// <summary>
		///		Indicates the email address of the author.
		/// </summary>
		[CanBeNull] public string Email { get; set; }

		/// <summary>
		///		Indicates the display name of the author.
		/// </summary>
		[NotNull] public string DisplayName { get; set; }

		/// <summary>
		///		Indicates the first name of the author.
		/// </summary>
		[CanBeNull] public string FirstName { get; set; }

		/// <summary>
		///		Indicates the last name of the author.
		/// </summary>
		[CanBeNull] public string LastName { get; set; }


		public WPAuthor(
			int authorID,
			[CanBeNull] string firstName,
			[CanBeNull] string lastName,
			[NotNull] string displayName,
			[CanBeNull] string email,
			[CanBeNull] string login)
		{
			AuthorID = authorID;
			FirstName = firstName;
			LastName = lastName;
			DisplayName = displayName.EnforceNotNull(nameof(displayName));
			Email = email;
			Login = login;
		}
		

		/// <inheritdoc />
		public void WriteToXmlStream(
			XmlStreamWriter writer)
		{
			var wpNs = FXNsRef.FromName("wp");

			writer.WriteStartElement(wpNs, "author")
			      .WriteInlineElement(wpNs, "author_id", AuthorID.ToString())
			      .WriteInlineElement(wpNs, "author_login", Login)
			      .WriteInlineElement(wpNs, "author_email", Email)
						.WriteInlineCDataElement(wpNs, "author_display_name", DisplayName)
			      .WriteInlineElement(wpNs, "author_first_name", FirstName)
			      .WriteInlineElement(wpNs, "author_last_name", LastName)
						.WriteEndElement();
		}
	}
}
