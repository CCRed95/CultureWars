using System.Text.RegularExpressions;

namespace CultureWars.Data.Domain.Complex
{
  public class PersonFactory
  {
    private static readonly Regex _nameRegex
      = new Regex(
        @"\A(?<first>[\w.]*)\b[^\w]*(?<middle>[\w\s.]*?)?(?<last>[\w.\']*)\Z");


    public static TPersonType CreatePerson<TPersonType>(
      string fullName)
        where TPersonType
        : Person,
          new()
    {
      var match = _nameRegex.Match(fullName);

      var person = new TPersonType();

      person.FirstName = match.Groups["first"].Value;
      person.MiddleName = match.Groups["middle"]?.Value;
      person.LastName = match.Groups["last"]?.Value;

      return person;
    }

  }
}
