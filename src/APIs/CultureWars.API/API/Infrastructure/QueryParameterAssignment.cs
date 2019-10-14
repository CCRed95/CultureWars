using CultureWars.API.Web;

namespace CultureWars.API.Infrastructure
{
  public class QueryParameterAssignment
    : IQueryParameterAssignment
  {
    public string ParameterName { get; }

    public string ArgumentValue { get; }


    public string GetFragment(bool start, bool end)
    {
      var fragment = $"{ParameterName}={ArgumentValue}";
      if (start)
      {
        fragment = $"?{fragment}";
      }
      if (end)
      {

        fragment += "&";
      }
      return fragment;
    }


    public QueryParameterAssignment(
      string parameterName,
      string argumentValue)
    {
      ParameterName = parameterName.UrlEncode();
      ArgumentValue = argumentValue.UrlEncode();
    }
  }
}