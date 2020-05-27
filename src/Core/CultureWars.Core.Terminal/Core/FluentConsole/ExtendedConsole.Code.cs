using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Ccr.Std.Core.Extensions;
using Ccr.Std.Core.TypeSystemInfo;
using JetBrains.Annotations;

namespace CultureWars.Core.FluentConsole
{
	internal class CodeLexemeWriter
	{

	}

	internal interface ICodeLiteralWriter
	{
		void WriteValueLiteralBase(object valueLiteralBase);
	}

	internal class CodeLiteralClassWriter<TValue>
		: ICodeLiteralWriter
			where TValue
				: class
	{
		public void WriteValueLiteral(
			TValue valueLiteral)
		{

		}

		/// <inheritdoc />
		void ICodeLiteralWriter.WriteValueLiteralBase(
			object valueLiteralBase)
		{
			if (valueLiteralBase == null)
			{
				WriteValueLiteral(null);
			}
			else if (valueLiteralBase is TValue value)
			{
				WriteValueLiteral(value);
			}
			else
			{
				throw new NotImplementedException();
			}
		}
	}

	internal class CodeLiteralWriter<TValue>
		: ICodeLiteralWriter
			where TValue
				: struct
	{
		private readonly Action<TValue> _literalFormatter;


		public CodeLiteralWriter(
			Action<TValue> literalFormatter)
		{
			_literalFormatter = literalFormatter;
		}

		
		public void WriteValueLiteral(
			TValue valueLiteral)
		{
			_literalFormatter.Invoke(valueLiteral);
		}

		/// <inheritdoc />
		void ICodeLiteralWriter.WriteValueLiteralBase(
			[NotNull] object valueLiteralBase)
		{
			valueLiteralBase.IsNotNull(nameof(valueLiteralBase));

			if (valueLiteralBase is TValue value)
			{
				WriteValueLiteral(value);
			}
		}
	}


	public partial class ExtendedConsole
	{
		private static readonly GeneralTextColorizationLayer _colorTheme = new GeneralTextColorizationLayer();


		private static readonly ConcurrentDictionary<SimpleCodeKind, Color> _codeKindToConsoleColorMap =
			new ConcurrentDictionary<SimpleCodeKind, Color>
			{
				[SimpleCodeKind.BracesAndOperators] = CodeClassification.ReSharperOperatorIdentifier.Foreground,
				[SimpleCodeKind.DefaultText] = CodeClassification.PlainText.Foreground,
				[SimpleCodeKind.BracesAndOperators] = CodeClassification.ReSharperBraceOutline.Foreground,
				[SimpleCodeKind.ClassIdentifier] = CodeClassification.ReSharperClassIdentifier.Foreground,
				[SimpleCodeKind.DelegateIdentifier] = CodeClassification.ReSharperDelegateIdentifier.Foreground,
				[SimpleCodeKind.EnumIdentifier] = CodeClassification.ReSharperEnumIdentifier.Foreground,
				[SimpleCodeKind.EventIdentifier] = CodeClassification.ReSharperEventIdentifier.Foreground,
				[SimpleCodeKind.FieldIdentifier] = CodeClassification.ReSharperFieldIdentifier.Foreground,
				[SimpleCodeKind.InterfaceIdentifier] = CodeClassification.ReSharperInterfaceIdentifier.Foreground,
				[SimpleCodeKind.MethodIdentifier] = CodeClassification.ReSharperMethodIdentifier.Foreground,
				//NamespaceIdentifier = 10,
				[SimpleCodeKind.ParameterIdentifier] = CodeClassification.ReSharperParameterIdentifier.Foreground,
				[SimpleCodeKind.PropertyIdentifier] = CodeClassification.ReSharperPropertyIdentifier.Foreground,
				//StructIdentifier = 13,
				[SimpleCodeKind.TypeParameterIdentifier] = CodeClassification.ReSharperTypeParameterIdentifier.Foreground,
				[SimpleCodeKind.VariableIdentifier] = CodeClassification.PlainText.Foreground,
				[SimpleCodeKind.Keyword] = CodeClassification.Keyword.Foreground,
				[SimpleCodeKind.Number] = CodeClassification.PlainText.Foreground,
				[SimpleCodeKind.String] = CodeClassification.String.Foreground,
			};


		public IExtendedConsoleSession WriteCodeLiteral<TObject>(
			TObject value)
		{
			switch (value)
			{
				case null:
					return WriteCode("null", CodeKind.Keyword);
				
				case bool @bool:
					return WriteCodeLiteral(@bool);

				case short @short:
					return WriteCodeLiteral(@short);

				case int @int:
					return WriteCodeLiteral(@int);

				case long @long:
					return WriteCodeLiteral(@long);

				case float @float:
					return WriteCodeLiteral(@float);
					
				case double @double:
					return WriteCodeLiteral(@double);

				case decimal @decimal:
					return WriteCodeLiteral(@decimal);

				case char @char:
					return WriteCodeLiteral(@char);

				case char[] charArr:
					return WriteCodeLiteral(charArr);

				case string @string:
					return WriteCodeLiteral(@string);

				default:
					break;
			}
			//if (value == null)
			//{
			//	return WriteCode()
			//}
			return this;
		}


		public IExtendedConsoleSession WriteCodeLiteral(
			bool value)
		{
			return WriteCode($"{value}", CodeKind.Keyword);
		}



		public IExtendedConsoleSession WriteCodeLiteral(
			short value)
		{
			return WriteCode($"{value}", CodeKind.Number);
		}

		public IExtendedConsoleSession WriteCodeLiteral(
			int value)
		{
			return WriteCode($"{value}", CodeKind.Number);
		}

		public IExtendedConsoleSession WriteCodeLiteral(
			long value)
		{
			return WriteCode($"{value}l", CodeKind.Number);
		}



		public IExtendedConsoleSession WriteCodeLiteral(
			float value)
		{
			return WriteCode($"{value}f", CodeKind.Number);
		}

		public IExtendedConsoleSession WriteCodeLiteral(
			double value)
		{
			return WriteCode($"{value}d", CodeKind.Number);
		}

		public IExtendedConsoleSession WriteCodeLiteral(
			decimal value)
		{
			return WriteCode($"{value}m", CodeKind.Number);
		}

		

		public IExtendedConsoleSession WriteCodeLiteral(
			char value)
		{
			return WriteCode($"'{value}'", CodeKind.String);
		}

		public IExtendedConsoleSession WriteCodeLiteral(
			char[] value)
		{
			return WriteCode($"\"{value}\"", CodeKind.String);
		}
		
		public IExtendedConsoleSession WriteCodeLiteral(
			string value)
		{
			return WriteCode($"\"{value}\"", CodeKind.String);
		}


		
		public IExtendedConsoleSession WriteCodeIdentifier(
			string value)
		{
			return WriteCode(value, CodeKind.ClassIdentifier);
		}


		//public IExtendedConsoleSession WriteCode(Type value)
		//{
		//	return this.PrintType(value);
		//}

		//public IExtendedConsoleSession WriteCodeMethod(string value)
		//{
		//	return this;
		//}

		//public IExtendedConsoleSession WriteCodeKeyword(string value)
		//{
		//	return this;
		//}

		public IExtendedConsoleSession WriteCodeWord(
			string lexeme)
		{
			//if (lexeme.IsNumber())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.Number);
			//}
			//else if (lexeme.IsStringLiteral())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.String);
			//}
			//else if (lexeme.IsCSharpKeyword())
			//{
			//	WriteCode(lexeme, SimpleCodeKind.Keyword);
			//}
			if (lexeme.IsValidCSharpIdentifier())
			{
				WriteCode(lexeme, CodeKind.FieldIdentifier); //wrong, properties
			}
			else
			{
				WriteCode(lexeme, CodeKind.BraceOperator);
			}

			return this;
		}

		public IExtendedConsoleSession WriteCode(
			string lexeme,
			SimpleCodeKind codeKind)
		{
			var color = _codeKindToConsoleColorMap[codeKind];

			return Write(lexeme, color);

			//var oldSystemColor = _log.ForegroundColor;

			//Console.ForegroundColor = colorManager.GetConsoleColor(color);
			//Console.Write(lexeme, color);
			//Console.ForegroundColor = oldSystemColor;
		}

		public IExtendedConsoleSession WriteCode(
			string lexeme,
			CodeKind codeKind)
		{
			var classification = _colorTheme.GetCodeClassification(codeKind);
			return Write(lexeme, classification.Foreground);
		}

		public IExtendedConsoleSession WriteCode(
			string lexeme,
			ComplexCodeKind complexCodeKind)
		{
			var classification = _colorTheme.GetCodeClassification(complexCodeKind);
			return Write(lexeme, classification.Foreground);
		}

		public IExtendedConsoleSession WriteType(
			Type type,
			bool fullyQualified = false)
		{
			if (fullyQualified)
			{
				if (type.Namespace != null)
				{
					var namespaceParts = type.Namespace.Split('.');

					foreach (var namespacePart in namespaceParts)
					{
						WriteCode(namespacePart, CodeKind.Keyword) // TODO namespace not keyword!
							.WriteCode(".", CodeKind.BraceOperator);
					}
				}
			}

			if (type.IsClass)
			{
				if (type.IsPrimitive)
				{
				}

				if (type.ContainsGenericParameters)
				{
					var genericTypeDefinition = type.GetGenericTypeDefinition();

					var genericTypeCount = genericTypeDefinition.GenericTypeArguments.Length;
					var currentTypeIndex = 0;

					WriteCode("<", CodeKind.BraceOperator);

					foreach (var typeArgument in genericTypeDefinition.GenericTypeArguments)
					{
						WriteSimpleType(typeArgument);
						if (currentTypeIndex <= genericTypeCount - 2)
							WriteCode(", ", CodeKind.BraceOperator);

						currentTypeIndex++;
					}

					WriteCode(">", CodeKind.BraceOperator);
				}
				else
				{
					WriteSimpleType(type);
				}
			}
			else if (type.IsInterface)
			{
				WriteCode(type.Name, CodeKind.InterfaceIdentifier);
			}
			else
			{
				WriteCode(type.Name, CodeKind.ClassIdentifier); //TODO struct
			}

			return this;
		}

		private IExtendedConsoleSession WriteSimpleType(
			Type type)
		{
			if (!type.IsClass || type.ContainsGenericParameters || type.IsInterface)
				throw new NotSupportedException();

			var isBuiltInType = TypeReference.BuiltInTypes.Contains(type);

			switch (type.Name)
			{
				case "Void":
				case "object":
					isBuiltInType = true;

					break;
			}

			var codeStyle = isBuiltInType
				? CodeKind.Keyword
				: CodeKind.ClassIdentifier;

			WriteCode(type.FormatName(), codeStyle);

			return this;
		}

		public IExtendedConsoleSession WriteNumber(
			string number)
		{
			if (number.Contains('.'))
			{
				if (!decimal.TryParse(number, out var value))
					throw new FormatException();

				WriteCode($"{value}", CodeKind.Number);
			}
			else
			{
				if (!long.TryParse(number, out var value))
					throw new FormatException();

				WriteCode($"{value}", CodeKind.Number);
			}

			return this;
		}

		public IExtendedConsoleSession WriteMethod(
			MethodInfo methodInfo)
		{
			if (methodInfo.IsPublic)
				WriteCode("public ", CodeKind.Keyword);

			if (methodInfo.IsPrivate)
				WriteCode("private ", CodeKind.Keyword);

			if (methodInfo.IsFamily)
				WriteCode("protected ", CodeKind.Keyword);

			if (methodInfo.IsAssembly)
				WriteCode("internal ", CodeKind.Keyword);

			if (methodInfo.IsStatic)
				WriteCode("static ", CodeKind.Keyword);

			if (methodInfo.IsAbstract)
				WriteCode("abstract ", CodeKind.Keyword);

			if (methodInfo.IsFinal)
				WriteCode("sealed ", CodeKind.Keyword);


			WriteType(methodInfo.ReturnType)
				.WriteCode(" ", CodeKind.BraceOperator)
				.WriteCode(methodInfo.Name, CodeKind.MethodIdentifier)
				.WriteCode("(", CodeKind.BraceOperator);

			var parametersCount = methodInfo.GetParameters()
				.Length;

			var currentParameterIndex = 0;

			foreach (var parameterInfo in methodInfo.GetParameters())
			{
				WriteParameter(parameterInfo);
				//WriteType(parameter.ParameterType)
				//	.WriteCode(" ", SimpleCodeKind.DefaultText)
				//	.WriteCode(parameter.Name, SimpleCodeKind.ParameterIdentifier);

				//if (parameter.HasDefaultValue)
				//{
				//	WriteCode(" ", SimpleCodeKind.DefaultText)
				//		.WriteCode("=", SimpleCodeKind.BracesAndOperators)
				//		.WriteCode(" ", SimpleCodeKind.DefaultText)
				//		.WriteCode(parameter.RawDefaultValue.ToString(), SimpleCodeKind.DefaultText);
				//}

				if (currentParameterIndex <= parametersCount - 2)
					WriteCode(", ", CodeKind.BraceOperator);

				currentParameterIndex++;
			}

			WriteCode(");", CodeKind.BraceOperator);

			return this;
		}

		public IExtendedConsoleSession WriteField(
			FieldInfo fieldInfo)
		{
			if (fieldInfo.IsPublic)
				WriteCode("public ", CodeKind.Keyword);

			if (fieldInfo.IsPrivate)
				WriteCode("private ", CodeKind.Keyword);

			if (fieldInfo.IsFamily)
				WriteCode("protected ", CodeKind.Keyword);

			if (fieldInfo.IsAssembly)
				WriteCode("internal ", CodeKind.Keyword);

			if (fieldInfo.IsStatic)
				WriteCode("static ", CodeKind.Keyword);

			if (fieldInfo.IsLiteral)
				WriteCode("const ", CodeKind.Keyword);

			if (fieldInfo.IsInitOnly)
				WriteCode("readonly ", CodeKind.Keyword);

			//var defaultValue = fieldInfo.GetRawConstantValue();

			WriteType(fieldInfo.FieldType)
				.WriteCode(" ", CodeKind.BraceOperator)
				.WriteCode(fieldInfo.Name, SimpleCodeKind.FieldIdentifier)
				.WriteCode(";", CodeKind.BraceOperator);

			return this;
		}

		public IExtendedConsoleSession WriteProperty(
			PropertyInfo propertyInfo)
		{
			var getMethodBase = propertyInfo.GetMethod;
			var setMethodBase = propertyInfo.SetMethod;

			WriteMethodBaseModifiers(getMethodBase);

			//var defaultValue = fieldInfo.GetRawConstantValue();
			//propertyInfo.
			WriteType(propertyInfo.PropertyType)
				.WriteCode(" ", CodeKind.BraceOperator)
				.WriteCode(propertyInfo.Name, CodeKind.FieldIdentifier)
				.WriteCode(" { ", CodeKind.BraceOperator);

			if (propertyInfo.CanRead)
				WriteCode("get", CodeKind.Keyword)
					.WriteCode("; ", CodeKind.BraceOperator);

			if (propertyInfo.CanWrite)
				WriteCode("set", CodeKind.Keyword)
					.WriteCode("; ", CodeKind.BraceOperator);

			WriteCode("}", CodeKind.BraceOperator);

			return this;
		}

		public IExtendedConsoleSession WriteMethodBaseModifiers(
			MethodBase methodBase)
		{
			if (methodBase.IsPublic)
				WriteCode("public ", CodeKind.Keyword);

			if (methodBase.IsPrivate)
				WriteCode("private ", CodeKind.Keyword);

			if (methodBase.IsFamily)
				WriteCode("protected ", CodeKind.Keyword);

			if (methodBase.IsAssembly)
				WriteCode("internal ", CodeKind.Keyword);

			if (methodBase.IsStatic)
				WriteCode("static ", CodeKind.Keyword);

			return this;
		}

		public IExtendedConsoleSession WriteAttribute<TAttribute>(
			params object[] ctorParameters)
		{
			var attributeName = typeof(TAttribute).Name;
			if (attributeName.EndsWith("Attribute"))
				attributeName = attributeName.Substring(0, attributeName.Length - "Attribute".Length);

			WriteCode("[", CodeKind.BraceOperator);
			WriteCode(attributeName, CodeKind.ClassIdentifier);

			if (ctorParameters.Any())
			{
				WriteCode("(", CodeKind.BraceOperator);


				WriteCode(")", CodeKind.BraceOperator);
			}

			WriteCode("]", CodeKind.BraceOperator);

			return this;
		}


		public IExtendedConsoleSession WriteConstructor(
			ConstructorInfo constructorInfo)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteEvent(
			EventInfo eventInfo)
		{
			throw new NotImplementedException();
		}

		public IExtendedConsoleSession WriteParameter(
			ParameterInfo parameterInfo)
		{
			WriteType(parameterInfo.ParameterType)
				.WriteCode(" ", SimpleCodeKind.BracesAndOperators)
				.WriteCode(parameterInfo.Name, SimpleCodeKind.ParameterIdentifier);

			if (parameterInfo.HasDefaultValue)
			{
				WriteCode(" ", SimpleCodeKind.BracesAndOperators)
					.WriteCode("=", SimpleCodeKind.BracesAndOperators)
					.WriteCode(" ", SimpleCodeKind.BracesAndOperators)
					.WriteCode(parameterInfo.RawDefaultValue.ToString(), SimpleCodeKind.BracesAndOperators);
			}
			return this;
		}

		public IExtendedConsoleSession BeginWriteXmlElement(
			string elementName)
		{
			Prefix()
				.WriteCode("<", SimpleCodeKind.BracesAndOperators)
				.WriteCode(elementName, SimpleCodeKind.ClassIdentifier);

			return this;
		}

		public IExtendedConsoleSession WriteXmlInlineParameter(
			string parameter,
			string value)
		{
			WriteCode(" ", SimpleCodeKind.BracesAndOperators)
				.WriteCode(parameter, SimpleCodeKind.PropertyIdentifier)
				.WriteCode("=", SimpleCodeKind.BracesAndOperators)
				.WriteCode("\"", SimpleCodeKind.String)
				.WriteCode(value, SimpleCodeKind.String)
				.WriteCode("\"", SimpleCodeKind.String);

			return this;
		}

		public IExtendedConsoleSession EndWriteXmlElement()
		{
			WriteCode("/>", SimpleCodeKind.BracesAndOperators)
				.WriteLine();

			return this;
		}

		public IExtendedConsoleSession EndWriteComplexXmlElement()
		{
			WriteCode(">", SimpleCodeKind.BracesAndOperators)
				.WriteLine();

			return this;
		}

		public IExtendedConsoleSession WriteEndComplexXmlElement(
			string elementName)
		{
			Prefix()
				.WriteCode("</", SimpleCodeKind.BracesAndOperators)
				.WriteCode(elementName, SimpleCodeKind.ClassIdentifier)
				.WriteCode(">", SimpleCodeKind.BracesAndOperators)
				.WriteLine();

			return this;
		}
	}
}

/*
 * 
		public IExtendedConsoleSession BeginWriteXmlElement(
			string elementName)
		{
			Prefix()
				.WriteCode("<", CodeKind.BraceOperator)
				.WriteCode(elementName, CodeKind.ClassIdentifier);

			return this;
		}

		public IExtendedConsoleSession WriteXmlInlineParameter(
			string parameter,
			string value)
		{
			WriteCode(" ", CodeKind.BraceOperator)
				.WriteCode(parameter, CodeKind.PropertyIdentifier)
				.WriteCode("=", CodeKind.BraceOperator)
				.WriteCode("\"", CodeKind.BraceOperator)
				.WriteCode(value, CodeKind.String)
				.WriteCode("\"", CodeKind.BraceOperator);

			return this;
		}

		public IExtendedConsoleSession EndWriteXmlElement()
		{
			WriteCode("/>", CodeKind.BraceOperator)
				.WriteLine();

			return this;
		}

		public IExtendedConsoleSession EndWriteComplexXmlElement()
		{
			WriteCode(">", CodeKind.BraceOperator)
				.WriteLine();

			return this;
		}

		public IExtendedConsoleSession WriteEndComplexXmlElement(
			string elementName)
		{
			Prefix()
				.WriteCode("</", CodeKind.BraceOperator)
				.WriteCode(elementName, CodeKind.ClassIdentifier)
				.WriteCode(">", CodeKind.BraceOperator)
				.WriteLine();

			return this;
		}
 */
//[SimpleCodeKind.BracesAndOperators] = Color.WhiteSmoke,
//[SimpleCodeKind.DefaultText] = Color.Cyan,
//[SimpleCodeKind.BracesAndOperators] = Color.DarkTurquoise,
//[SimpleCodeKind.ClassIdentifier] = Color.MediumSeaGreen,
//[SimpleCodeKind.DelegateIdentifier] = Color.GhostWhite,
//[SimpleCodeKind.EnumIdentifier] = Color.MediumAquamarine,
//[SimpleCodeKind.EventIdentifier] = Color.OrangeRed,
//[SimpleCodeKind.FieldIdentifier] = Color.MediumPurple,
//[SimpleCodeKind.InterfaceIdentifier] = Color.DarkOrange,
//[SimpleCodeKind.MethodIdentifier] = Color.DodgerBlue,
////NamespaceIdentifier = 10,
//[SimpleCodeKind.ParameterIdentifier] = Color.Turquoise,
//[SimpleCodeKind.PropertyIdentifier] = Color.SkyBlue,
////StructIdentifier = 13,
//[SimpleCodeKind.TypeParameterIdentifier] = Color.Yellow,
//[SimpleCodeKind.VariableIdentifier] = Color.LightSkyBlue,
//[SimpleCodeKind.Keyword] = Color.Magenta,
//[SimpleCodeKind.Number] = Color.White,
//[SimpleCodeKind.String] = Color.Red