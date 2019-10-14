using System.Threading.Tasks;

namespace CultureWars.API.Web
{
  public interface IHttpClient
  {
    Task<string> GetContentAsync(string address);
  }
}