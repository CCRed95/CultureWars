namespace CultureWars.Core.FluentConsole
{
	/// <summary>
	/// Exposes methods used for creating (potentially inexact) copies of objects.
	/// </summary>
	/// <typeparam name="TSelf"></typeparam>
	public interface IPrototypable<TSelf>
	{
		/// <summary>
		///		Returns a potentially inexact copy of the target object.
		/// </summary>
		/// <returns></returns>
		TSelf Prototype();
	}
}