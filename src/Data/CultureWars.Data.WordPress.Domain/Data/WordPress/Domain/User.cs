using System;
namespace CultureWars.Data.WordPress.Domain
{
	public class User
	{
		public ulong UserId { get; set; }

		public string UserLogin { get; set; }
		
		public string UserPass { get; set; }
		
		public string UserNicename { get; set; }
		
		public string UserEmail { get; set; }
		
		public string UserUrl { get; set; }
		
		public DateTime UserRegistered { get; set; }
		
		public string UserActivationKey { get; set; }
		
		public int UserStatus { get; set; }
		
		public string DisplayName { get; set; }
	}
}
