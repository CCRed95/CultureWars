using System.Text;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;

namespace CultureWars.Data.Domain.Complex
{
  public abstract class Person
  {
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    public string LastName { get; set; }

		public string AlternateName { get; set; }

		
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


		protected Person()
		{
		}

		protected Person(
	    string firstName,
	    string middleName,
	    string lastName)
			: this()
    {
	    FirstName = firstName.EnforceNotNull(nameof(firstName));
	    MiddleName = middleName.EnforceNotNull(nameof(middleName));
	    LastName = lastName.EnforceNotNull(nameof(lastName));
    }

    protected Person(
	    string firstName,
	    string middleName,
	    string lastName,
	    string alternateName)
				: this(
					firstName,
					middleName,
					lastName)
    {
	    AlternateName = alternateName.EnforceNotNull(nameof(alternateName));
    }
	}
}
