using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CultureWars
{
	public abstract class InheritingCodeColorizationLayer<TBase>
		: CodeColorizationLayer
			where TBase
				: CodeColorizationLayer
	{
		public abstract IDictionary<
			Expression<Func<CodeClassification>>,
			Expression<Func<TBase, CodeClassification>>> InheritanceMaps { get; }
	}
}