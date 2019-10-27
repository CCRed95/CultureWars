using System.Runtime.CompilerServices;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public partial class CultureWarsTag
	{
		/// <summary>
		///		Indicates the Category Name.
		/// </summary>
		[NotNull]
		public string TagName { get; set; }

		/// <summary>
		///		Indicates the Category Nice/Friendly Name.
		/// </summary>
		[NotNull]
		public string TagFriendlyName { get; set; }

		/// <summary>
		///		Indicates the Category Name's parent name, if applicable.
		/// </summary>
		[NotNull]
		public string HtmlEncodedTagName { get; set; }



		public CultureWarsTag(
			[NotNull] string tagFriendlyName)
		{
			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName));

			TagName = TagFriendlyName
				.Replace(".", "")
				.Replace("-", "")
				.Replace(".", "")
				.Replace(" ", "");

			HtmlEncodedTagName = TagFriendlyName
				.Replace(" ", "+");
		}

		public CultureWarsTag(
			[NotNull] string tagFriendlyName,
			[NotNull] string htmlEncodedTagName,
			[CallerMemberName] string tagName = "")
		{
			TagName = tagName.EnforceNotNull(nameof(tagName));
			TagFriendlyName = tagFriendlyName.EnforceNotNull(nameof(tagFriendlyName));
			HtmlEncodedTagName = htmlEncodedTagName.EnforceNotNull(nameof(htmlEncodedTagName));
		}
	}
}