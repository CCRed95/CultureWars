namespace CultureWars.Core.Builders
{
	public interface IFluentBuilder<out TBuilds>
	{
		TBuilds Build();
	}
}
