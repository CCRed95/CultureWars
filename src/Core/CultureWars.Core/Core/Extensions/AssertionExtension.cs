using System;
using System.Runtime.CompilerServices;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Core.Extensions
{
	public static class AssertionExtensions
	{
		/// <summary>
		///   Assertion enforcement method that ensures that <paramref name="this"/> is not null
		/// </summary>
		/// <param name="this">
		///   The object in which to ensure non-nullability upon
		/// </param>
		/// <param name="elementName">
		///   The parameter name of the object in which to ensure non-nullability upon
		/// </param>
		/// <param name="callerMemberName">
		///   Compiler-provided string of the method name containing this call
		/// </param>
		[ContractAnnotation("this:null => halt"), AssertionMethod]
		public static TValue EnforceNotNull<TValue>(
			[AssertionCondition(AssertionConditionType.IS_NOT_NULL), NotNull] this TValue @this,
			[InvokerParameterName] string elementName,
			[CallerMemberName] string callerMemberName = "")
		{
			if (@this == null)
				throw new ArgumentNullException(
					elementName,
					$"Parameter {elementName.SQuote()} passed to the " +
					$"method {callerMemberName.SQuote()} cannot be null.");

			return @this;
		}
	}
}
