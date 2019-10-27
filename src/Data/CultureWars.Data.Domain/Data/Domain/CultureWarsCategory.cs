using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
	public partial class CultureWarsCategory
	{
		/// <summary>
		///		Indicates the Category Name.
		/// </summary>
		[NotNull] public string CategoryName { get; set; }

		/// <summary>
		///		Indicates the Category Nice/Friendly Name.
		/// </summary>
		[NotNull] public string CategoryNiceName { get; set; }

		/// <summary>
		///		Indicates the Category Name's parent name, if applicable.
		/// </summary>
		[CanBeNull] public string CategoryParent { get; set; }


		public CultureWarsCategory(
			[NotNull] string categoryName,
			[NotNull] string categoryNiceName,
			[CanBeNull] string categoryParent = null)
		{
			CategoryName = categoryName.EnforceNotNull(nameof(categoryName));
			CategoryNiceName = categoryNiceName.EnforceNotNull(nameof(categoryNiceName));
			CategoryParent = categoryParent;
		}
	}
}