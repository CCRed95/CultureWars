namespace CultureWars.API.Infrastructure
{
  public interface INamedAnchorFragment
    : IUriFragment
  {
    string AnchorValue { get; }
  }
}
