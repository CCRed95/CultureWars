namespace CultureWars.API.Common.Materializers
{
	public interface IDomainEntityMaterializer
	{
		object MaterializeBase(object data);
	}
}