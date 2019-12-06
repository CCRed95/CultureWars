using System.Collections.Generic;
using System.Linq;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Extensions
{
	public static class LinqExtensions
	{
		public static IEnumerable<IEnumerable<TValue>> Chunk<TValue>(
			[NotNull, ItemNotNull] this IEnumerable<TValue> @this,
			int chunkSize)
		{
			@this.IsNotNull(nameof(@this));

			var pos = 0;
			while (@this.Skip(pos).Any())
			{
				yield return @this.Skip(pos).Take(chunkSize);
				pos += chunkSize;
			}
		}
	}
}
