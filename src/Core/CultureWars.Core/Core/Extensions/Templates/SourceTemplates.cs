using JetBrains.Annotations;
using Ccr.Std.Core.Extensions;

#pragma warning disable IDE1006

namespace CultureWars.Core.Extensions.Templates
{
	public static class SourceTemplates
	{
		/// <summary>
		///		This ReSharper Source Post-Fix Template Extension Method generates an assignment statement
		///		upon the provided <paramref name="propertyName"/> to the <paramref name="argumentName"/>
		///		value, while performing a null-check enforcement upon the value in a singular statement. 
		/// </summary>
		/// <remarks>
		///		The <see cref="M:innp"/> Extension Method is a ReSharper source template that acts as a
		///		post-fix ReSharper template decorated with the <see cref="T:SourceTemplateAttribute"/>
		///		and contains <see cref="T:MacroAttribute"/>, non-editable parameters.
		/// </remarks>
		/// <typeparam name="TValue">
		///		A generic type indicating the type of the extension method's subject parameter.
		/// </typeparam>
		/// <param name="propertyName">
		///		The subject of the extension method, indicating the PropertyName that is being assigned to.
		/// </param>
		/// <param name="argumentName">
		///		A <see cref="string"/> value that is a camel-cased, first character-decapitalized version
		///		of the <paramref name="propertyName"/> value. This parameter does not need to be assigned,
		///		rather it is generated through the <see cref="N:JetBrains.Annotations"/> and
		///		<see cref="T:MacroAttribute"/>-driven macro system.
		/// </param>
		/// <returns>
		///		This method returns <see langword="T:void"/>, and generates an assignment statement upon
		///		the <see cref="propertyName"/>, while performing a null-check enforcement upon the subject
		///		<paramref name="argumentName"/> value in the same statement.
		/// </returns>
		[SourceTemplate]
		public static void innp<TValue>(
			this TValue propertyName,
			[Macro(Expression = "decapitalize($propertyName$)", Editable = -1)] string argumentName)
		{
			/*$$propertyName$ = $argumentName$.EnforceNotNull(nameof($argumentName$));*/
		}

		/// <summary>
		///		This ReSharper Source Post-Fix Template Extension Method generates an assignment statement
		///		upon the macro-provided <paramref name="propertyName"/> to the <paramref name="argumentName"/>
		///		value, while performing a null-check enforcement upon the value in a singular statement.  
		/// </summary>
		/// <remarks>
		///		The <see cref="M:innp"/> Extension Method is a ReSharper source template that acts as a
		///		post-fix ReSharper template decorated with the <see cref="T:SourceTemplateAttribute"/>
		///		and contains <see cref="T:MacroAttribute"/>, non-editable parameters.
		/// </remarks>
		/// <typeparam name="TValue">
		///		A generic type indicating the type of the extension method's subject parameter.
		/// </typeparam>
		/// <param name="argumentName">
		///		The subject of the extension method, indicating the argument name that is being assigned from.
		/// </param>
		/// <param name="propertyName">
		///		A <see cref="string"/> value that is a pascal-cased, first character-capitalized version
		///		of the <paramref name="argumentName"/> value. This parameter does not need to be assigned,
		///		rather it is generated through the <see cref="N:JetBrains.Annotations"/> and
		///		<see cref="T:MacroAttribute"/>-driven macro system.
		/// </param>
		/// <returns>
		///		This method returns <see langword="void"/>, and generates an assignment statement upon
		///		the <see cref="propertyName"/>, while performing a null-check enforcement upon the subject
		///		<paramref name="argumentName"/> value in the same statement.
		/// </returns>
		[SourceTemplate]
		public static void inna<TValue>(
			this TValue argumentName,
			[Macro(Expression = "capitalize($argumentName$)", Editable = -1)] string propertyName)
		{
			/*$$propertyName$ = $argumentName$.EnforceNotNull(nameof($argumentName$));*/
		}

		/// <summary>
		///		This ReSharper Source Post-Fix Template Extension Method generates a simple statement
		///		upon the provided <paramref name="argumentName"/> that asserts its non-nullability.
		/// </summary>
		/// <remarks>
		///		The <see cref="M:inn"/> Extension Method is a ReSharper source template that acts as a
		///		post-fix ReSharper template decorated with the <see cref="T:SourceTemplateAttribute"/>.
		/// </remarks>
		/// <param name="argumentName">
		///		The subject of the extension method, indicating the argument name that is being assigned from.
		/// </param>
		/// <returns>
		///		This method returns <see langword="T:void"/>, and generates an simple null-check assertment
		///		statement upon the <see cref="argumentName"/>.
		/// </returns>
		[SourceTemplate]
		public static void inn(
			this object argumentName)
		{
			argumentName.IsNotNull(nameof(argumentName));
		}
	}
}

#pragma warning restore IDE1006



//[SourceTemplate]
//public static TValue gnna<TValue>(
//	this TValue name,
//	[Macro(Expression = "decapitalize($name$)", Editable = -1)] string backingName)
//{
//	return name.EnforceNotNull(nameof(name));
//}


//[SourceTemplate]
//public static TValue innp<TValue>(
//	this TValue propertyName,
//	[Macro(Expression = "decapitalize($name$)", Editable = -1)] string backingName)
//{
//	return name.EnforceNotNull(nameof(name));
//}