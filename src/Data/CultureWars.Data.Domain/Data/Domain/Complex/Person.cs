using System.Text;
using Ccr.Std.Core.Extensions;

namespace CultureWars.Data.Domain.Complex
{
  public abstract class Person
  {
    protected class PersonImpl
      : Person
    {
      
    }

    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

		

    //[NotMapped]
    public string FullName
    {
      get
      {
        var sb = new StringBuilder();

        sb.Append(FirstName);

        if (!MiddleName.IsNullOrEmptyEx())
        {
          sb.Append(" ");
          sb.Append(MiddleName);
        }
        if (!LastName.IsNullOrEmptyEx())
        {
          sb.Append(" ");
          sb.Append(LastName);
        }
        return sb.ToString();
      }
      set
      {
        var person = PersonFactory.CreatePerson<PersonImpl>(value);

        FirstName = person.FirstName;
        MiddleName = person.MiddleName;
        LastName = person.LastName;
        //GenderID = person.GenderID;

      }
    }

    public string AlternateName { get; set; }
    
  }
}
