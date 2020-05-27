using System;

namespace CultureWars
{
	public class GroupNameAttribute
		: Attribute
	{
		public string GroupName { get; }


		public GroupNameAttribute(
			string groupName)
		{
			GroupName = groupName;
		}
	}
}