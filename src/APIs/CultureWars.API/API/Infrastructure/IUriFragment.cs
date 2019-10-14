namespace CultureWars.API.Infrastructure
{
  public interface IUriFragment
  {
    string GetFragment(bool start, bool end);
  }
}
