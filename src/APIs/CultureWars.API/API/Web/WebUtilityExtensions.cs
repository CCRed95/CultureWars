using System;
using System.Net;

namespace CultureWars.API.Web
{
  public static class WebUtilityExtensions
  {
    public static string UrlDecode(
      this string @this)
    {
      return WebUtility.UrlDecode(@this);
    }

    public static string UrlEncode(
      this string @this)
    {
	    return Uri.EscapeUriString(@this);
      return WebUtility.UrlEncode(@this);
    }
  }
}
