namespace CultureWars.Core.FluentConsole.Colorization.Stylization
{
	/// <summary>
	///		Defines a string transformation.
	/// </summary>
	/// <param name="unstyledInput">
	///		The entire input string being matched against, before styling has taken place.
	/// </param>
	/// <param name="matchLocation">
	///		The location of the target in the input string.
	/// </param>
	/// <param name="match">
	///		The "matching" portion of the input string.
	/// </param>
	/// <returns>
	///		A transformed version of the 'match' parameter.
	/// </returns>
	public delegate string MatchFound(
		string unstyledInput,
		TextMatchDescriptor matchLocation,
		string match);
}