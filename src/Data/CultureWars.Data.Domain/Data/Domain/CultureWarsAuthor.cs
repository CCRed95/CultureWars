using CultureWars.Core.Extensions;
using CultureWars.Data.Domain.Complex;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	/// <summary>
	///		The type declaration for the <see cref="CultureWarsAuthor"/> entity.
	/// </summary>
	public partial class CultureWarsAuthor
		: Person
	{
		/// <summary>
		///		Indicates the email address of the author.
		/// </summary>
		[NotNull] public string AuthorEmail { get; set; }
		
		/// <summary>
		///		The internal CultureWars.com author Id.
		/// </summary>
		public int AuthorID { get; set; }

		/// <summary>
		///		The internal CultureWars.com author Login.
		/// </summary>
		[NotNull] public string AuthorLogin { get; set; }


		/// <summary>
		///		The private constructor for the <see cref="CultureWarsAuthor"/> type.
		/// </summary>
		/// <param name="firstName">
		///		<inheritdoc cref="firstName"/>
		/// </param>
		/// <param name="middleName">
		///		<inheritdoc cref="middleName"/>
		/// </param>
		/// <param name="lastName">
		///		<inheritdoc cref="lastName"/>
		/// </param>
		/// <param name="authorEmail">
		///		Indicates the email address of the author.
		/// </param>
		/// <param name="authorID">
		///		The internal CultureWars.com author Id.
		/// </param>
		/// <param name="authorLogin">
		///		The internal CultureWars.com author Login.
		/// </param>
		/// <inheritdoc/>
		private CultureWarsAuthor(
			[NotNull] string firstName,
			[NotNull] string middleName,
			[NotNull] string lastName,
			[NotNull] string authorEmail,
			int authorID,
			[NotNull] string authorLogin)
				: base(
					firstName,
					middleName,
					lastName)
		{
			AuthorEmail = authorEmail.EnforceNotNull(nameof(authorEmail));
			AuthorID = authorID.EnforceNotNull(nameof(authorID));
			AuthorLogin = authorLogin.EnforceNotNull(nameof(authorLogin));
		}

		private CultureWarsAuthor(
			[NotNull] string firstName,
			[NotNull] string middleName,
			[NotNull] string lastName,
			[NotNull] string alternateName,
			[NotNull] string authorEmail,
			int authorID,
			[NotNull] string authorLogin)
				: this(
					firstName,
					middleName,
					lastName,
					authorEmail,
					authorID,
					authorLogin)
		{
			AlternateName = alternateName.EnforceNotNull(nameof(alternateName));
		}
	}
}
