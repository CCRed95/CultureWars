namespace CultureWars.Data.Domain
{
	/// <summary>
	///		Static Declarations of the <see cref="CultureWarsAuthor"/> type.
	/// </summary>
	public partial class CultureWarsAuthor
	{
		/// <summary>
		///		Dr. E. Michael Jones Author Entity Declaration
		/// </summary>
		public static readonly CultureWarsAuthor EMichaelJones
			= new CultureWarsAuthor(
				"Eugene",
				"Michael",
				"Jones",
				"Dr. E. Michael Jones",
				"Jones@CultureWars.com",
				1060845357,
				"E.MichaelJones");
	}
}