using System;
using System.Linq.Expressions;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole.Extensions
{
	public static class ExpressionExtensions
	{
		public static Expression<Action<TOwner, TField>> ToFieldSetterExpression<TOwner, TField>(
			[NotNull] this Expression<Func<TOwner, TField>> @this)
		{
			if (!(@this.Body is MemberExpression memberExpression))
				throw new NotSupportedException(
					$"The parameter {nameof(@this).SQuote()}'s {nameof(LambdaExpression.Body).SQuote()} " +
					$"property value is of type {@this.Body.Type.Name.SQuote()}, and is not supported . " +
					$"The embodied expression type should be a {nameof(MemberExpression).SQuote()}");

			var fieldParameterExpression = Expression.Parameter(
				typeof(TField));

			var ownerParameterExpression = Expression.Parameter(
				typeof(TOwner));

			var fieldGetterExpression = Expression.Field(
				ownerParameterExpression,
				memberExpression.Member.Name);

			var assignmentExpression = Expression.Assign(
				fieldGetterExpression,
				fieldParameterExpression);

			var setterExpression = Expression.Lambda<Action<TOwner, TField>>(
				assignmentExpression,
				ownerParameterExpression,
				fieldParameterExpression);

			return setterExpression;
		}
	}
}
