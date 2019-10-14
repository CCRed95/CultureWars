using System;
using System.Collections.Generic;

namespace CultureWars.API.Common.Materializers
{
	public static class EntityMaterializerContext
	{
		public static IDictionary<(Type, Type), IDomainEntityMaterializer> _mapping 
			= new Dictionary<(Type, Type), IDomainEntityMaterializer>();
    

		public static void Register<TDomainEntityMaterializer, TEntity, TData>(
			TDomainEntityMaterializer instance)
				where TDomainEntityMaterializer
					: DomainEntityMaterializer<
						TEntity,
						TData>
		{
			_mapping.Add(
				(typeof(TEntity), typeof(TData)), 
				instance);
		}
	}
}