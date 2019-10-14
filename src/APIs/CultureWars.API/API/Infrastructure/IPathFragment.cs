namespace CultureWars.API.Infrastructure
{
  public interface IPathFragment
    : IUriFragment
  {
    string Path { get; }
  }
}