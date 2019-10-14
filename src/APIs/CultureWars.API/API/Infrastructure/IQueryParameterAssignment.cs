namespace CultureWars.API.Infrastructure
{
  public interface IQueryParameterAssignment
    : IUriFragment
  {
    string ParameterName { get; }

    string ArgumentValue { get; }
  }
}