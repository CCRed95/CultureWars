using System.Collections.Generic;
using System.Linq;
using CultureWars.API.CultureWars.Domain;
using CultureWars.API.CultureWars.Interpreters;
using CultureWars.API.Web;

namespace CultureWars.API.CultureWars
{
	public class CultureWarsAPI
	{
		public static IEnumerable<CWMVolume> QueryVolumes()
		{
			return CWMInterpreter.ScrapeVolumes();
		}
	}
}