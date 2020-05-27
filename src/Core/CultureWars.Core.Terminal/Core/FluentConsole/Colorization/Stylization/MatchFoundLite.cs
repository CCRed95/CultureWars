namespace CultureWars.Core.FluentConsole.Colorization.Stylization
{
	/// <summary>
	///	Defines a simpler string transformation.
	/// </summary>
	/// <param name="match">
	///	The "matching" portion of the input string.
	/// </param>
	/// <returns>
	///	A transformed version of the 'match' parameter.
	/// </returns>
	public delegate string MatchFoundLite(
		string match);
}