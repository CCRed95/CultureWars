using System;

namespace CultureWars.API.Smmry.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SmmryParameterAttribute 
		: Attribute
	{
		public string Name { get; set; }

		public bool HasParameter { get; }


		public SmmryParameterAttribute(
			string name, 
			bool hasParameter)
		{
			Name = name;
			HasParameter = hasParameter;
		}


		public override string ToString()
		{
			return Name;
		}
	}
}
