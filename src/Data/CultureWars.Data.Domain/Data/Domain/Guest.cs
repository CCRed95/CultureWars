using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CultureWars.Data.Domain.Complex;
using JetBrains.Annotations;

namespace CultureWars.Data.Domain
{
  public partial class Guest
    : Person
  {
    public int GuestID { get; set; }

    public string Description { get; set; }

    public string TwitterHandle { get; set; }

    public string WebsiteUrl { get; set; }

    public string HeadshotImagePath { get; set; }


    public virtual ICollection<GuestAppearance> ShowAppearances { get; set; }


		[UsedImplicitly]
		public Guest()
		{
			ShowAppearances = new HashSet<GuestAppearance>();
		}

		public Guest(
      [NotNull] string fullName)
        : this()
    {
      FullName = fullName;
		}
		
    public Guest(
      [NotNull] string fullName,
      [CanBeNull] string description,
      [CanBeNull] string twitterHandle,
      [CanBeNull] string websiteUrl,
      [CanBeNull] string headshotImagePath)
        : this(
          fullName)
    {
      Description = description;
      TwitterHandle = twitterHandle;
      WebsiteUrl = websiteUrl;
      HeadshotImagePath = headshotImagePath;
    }
		

    public static Guest Define(
      [CallerLineNumber] int callerLineNumber = 0,
      [CallerMemberName] string callerMemberName = "")
    {
      var guest = PersonFactory.CreatePerson<Guest>(
        callerMemberName);
      return guest;
    }
  }
}