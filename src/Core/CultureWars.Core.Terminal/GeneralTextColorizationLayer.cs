using System;
using System.Collections.Generic;

namespace CultureWars
{
	public class GeneralTextColorizationLayer
		: CodeColorizationLayer
	{
		private static readonly IDictionary<ComplexCodeKind, Func<GeneralTextColorizationLayer, CodeClassification>> _complexCodeKindMap
			= new Dictionary<ComplexCodeKind, Func<GeneralTextColorizationLayer, CodeClassification>>
			{
				[ComplexCodeKind.PlainText] = t => t.PlainText,
				[ComplexCodeKind.Brace] = t => t.Brace,
				[ComplexCodeKind.Comment] = t => t.Comment,
				[ComplexCodeKind.NonStaticClassIdentifier] = t => t.NonStaticClassIdentifier,
				[ComplexCodeKind.StaticClassIdentifier] = t => t.StaticClassIdentifier,
				[ComplexCodeKind.ConstantIdentifier] = t => t.ConstantIdentifier,
				[ComplexCodeKind.DelegateIdentifier] = t => t.DelegateIdentifier,
				[ComplexCodeKind.EnumIdentifier] = t => t.EnumIdentifier,
				[ComplexCodeKind.EnumMemberIdentifier] = t => t.EnumMemberIdentifier,
				[ComplexCodeKind.EventIdentifier] = t => t.EventIdentifier,
				[ComplexCodeKind.ExtensionMethodIdentifier] = t => t.ExtensionMethodIdentifier,
				[ComplexCodeKind.FieldIdentifier] = t => t.FieldIdentifier,
				[ComplexCodeKind.InterfaceIdentifier] = t => t.InterfaceIdentifier,
				[ComplexCodeKind.LocalMethodIdentifier] = t => t.LocalMethodIdentifier,
				[ComplexCodeKind.MethodIdentifier] = t => t.MethodIdentifier,
				[ComplexCodeKind.NamespaceIdentifier] = t => t.NamespaceIdentifier,
				[ComplexCodeKind.ParameterIdentifier] = t => t.ParameterIdentifier,
				[ComplexCodeKind.PathIdentifier] = t => t.PathIdentifier,
				[ComplexCodeKind.PropertyIdentifier] = t => t.PropertyIdentifier,
				[ComplexCodeKind.TypeParameterIdentifier] = t => t.TypeParameterIdentifier,
				[ComplexCodeKind.ValueTupleComponentIdentifier] = t => t.ValueTupleComponentIdentifier,
				[ComplexCodeKind.VariableLocalImmutableIdentifier] = t => t.VariableLocalImmutableIdentifier,
				[ComplexCodeKind.MutableLocalVariableIdentifier] = t => t.MutableLocalVariableIdentifier,
				[ComplexCodeKind.Keyword] = t => t.Keyword,
				[ComplexCodeKind.Number] = t => t.Number,
				[ComplexCodeKind.Operator] = t => t.Operator,
				[ComplexCodeKind.RegexComment] = t => t.RegexComment,
				[ComplexCodeKind.RegexGroup] = t => t.RegexGroup,
				[ComplexCodeKind.RegexIdentifier] = t => t.RegexIdentifier,
				[ComplexCodeKind.RegexMatchedSelection] = t => t.RegexMatchedSelection,
				[ComplexCodeKind.RegexMatchedValue] = t => t.RegexMatchedValue,
				[ComplexCodeKind.RegexSet] = t => t.RegexSet,
				[ComplexCodeKind.String] = t => t.String,
				[ComplexCodeKind.StringFormatItem] = t => t.StringFormatItem,
				[ComplexCodeKind.StringVerbatim] = t => t.StringVerbatim,
				[ComplexCodeKind.StringEscapeCharacter1] = t => t.StringEscapeCharacter1,
				[ComplexCodeKind.StringEscapeCharacter2] = t => t.StringEscapeCharacter2,
			};

		private static readonly IDictionary<CodeKind, Func<GeneralTextColorizationLayer, CodeClassification>> _codeKindMap
				= new Dictionary<CodeKind, Func<GeneralTextColorizationLayer, CodeClassification>>
				{
					[CodeKind.BraceOperator] = t => t.Operator,
					[CodeKind.ClassIdentifier] = t => t.NonStaticClassIdentifier,
					[CodeKind.ConstantIdentifier] = t => t.ConstantIdentifier,
					[CodeKind.DelegateIdentifier] = t => t.DelegateIdentifier,
					[CodeKind.EnumIdentifier] = t => t.EnumIdentifier,
					[CodeKind.EventIdentifier] = t => t.EventIdentifier,
					[CodeKind.FieldIdentifier] = t => t.FieldIdentifier,
					[CodeKind.InterfaceIdentifier] = t => t.InterfaceIdentifier,
					[CodeKind.MethodIdentifier] = t => t.MethodIdentifier,
					[CodeKind.NamespaceIdentifier] = t => t.NamespaceIdentifier,
					[CodeKind.ParameterIdentifier] = t => t.ParameterIdentifier,
					[CodeKind.PropertyIdentifier] = t => t.PropertyIdentifier,
					[CodeKind.TypeParameterIdentifier] = t => t.TypeParameterIdentifier,
					[CodeKind.Keyword] = t => t.Keyword,
					[CodeKind.Number] = t => t.Number,
					[CodeKind.String] = t => t.String,
				};

		public CodeClassification GetCodeClassification(ComplexCodeKind complexCodeKind)
		{
			if (!_complexCodeKindMap.TryGetValue(complexCodeKind, out var func))
				throw new KeyNotFoundException();

			var value = func(this);
			return value;
		}

		public CodeClassification GetCodeClassification(CodeKind codeKind)
		{
			if (!_codeKindMap.TryGetValue(codeKind, out var func))
				throw new KeyNotFoundException();

			var value = func(this);
			return value;
		}


		/// <inheritdoc />
		public override string LayerName
		{
			get => $"General Text";
		}


		public virtual CodeClassification PlainText
		{
			get => CodeClassification.PlainText;
		}
		
		public virtual CodeClassification Brace
		{
			get => CodeClassification.ReSharperOperatorIdentifier; // wrong
		}

		public virtual CodeClassification Comment
		{
			get => CodeClassification.Comment;
		}

		public virtual CodeClassification NonStaticClassIdentifier
		{
			get => CodeClassification.ReSharperClassIdentifier;
		}

		public virtual CodeClassification StaticClassIdentifier
		{
			get => CodeClassification.ReSharperStaticClassIdentifier;
		}

		public virtual CodeClassification ConstantIdentifier
		{
			get => CodeClassification.ReSharperConstantIdentifier;
		}

		public virtual CodeClassification DelegateIdentifier
		{
			get => CodeClassification.ReSharperDelegateIdentifier;
		}

		public virtual CodeClassification EnumIdentifier
		{
			get => CodeClassification.ReSharperEnumIdentifier;
		}

		public virtual CodeClassification EnumMemberIdentifier
		{
			get => CodeClassification.ReSharperEnumIdentifier; //wrong
		}

		public virtual CodeClassification EventIdentifier
		{
			get => CodeClassification.ReSharperEventIdentifier;
		}

		public virtual CodeClassification ExtensionMethodIdentifier
		{
			get => CodeClassification.ReSharperExtensionMethodIdentifier;
		}

		public virtual CodeClassification FieldIdentifier
		{
			get => CodeClassification.ReSharperFieldIdentifier;
		}

		public virtual CodeClassification InterfaceIdentifier
		{
			get => CodeClassification.ReSharperInterfaceIdentifier;
		}

		public virtual CodeClassification LocalMethodIdentifier
		{
			get => CodeClassification.ReSharperMethodIdentifier;//wrong
		}

		public virtual CodeClassification MethodIdentifier
		{
			get => CodeClassification.ReSharperMethodIdentifier;
		}

		public virtual CodeClassification NamespaceIdentifier
		{
			get => CodeClassification.ReSharperNamespaceIdentifier;
		}

		public virtual CodeClassification ParameterIdentifier
		{
			get => CodeClassification.ReSharperParameterIdentifier;
		}

		public virtual CodeClassification PathIdentifier
		{
			get => CodeClassification.ReSharperPathIdentifier;
		}

		public virtual CodeClassification PropertyIdentifier
		{
			get => CodeClassification.ReSharperPropertyIdentifier;
		}

		public virtual CodeClassification TypeParameterIdentifier
		{
			get => CodeClassification.ReSharperTypeParameterIdentifier;
		}

		public virtual CodeClassification ValueTupleComponentIdentifier
		{
			get => CodeClassification.ReSharperTupleComponentIdentifier;
		}

		public virtual CodeClassification VariableLocalImmutableIdentifier
		{
			get => CodeClassification.ReSharperMutableLocalVariableIdentifier; //wrong?
		}

		public virtual CodeClassification MutableLocalVariableIdentifier
		{
			get => CodeClassification.ReSharperMutableLocalVariableIdentifier;
		}

		public virtual CodeClassification Keyword
		{
			get => CodeClassification.Keyword;
		}

		public virtual CodeClassification Number
		{
			get => CodeClassification.ReSharperOperatorIdentifier;//wrong
		}

		public virtual CodeClassification Operator
		{
			get => CodeClassification.ReSharperOperatorIdentifier;
		}

		public virtual CodeClassification RegexComment
		{
			get => CodeClassification.ReSharperRegexComment;
		}

		public virtual CodeClassification RegexGroup
		{
			get => CodeClassification.ReSharperRegexGroup;
		}

		public virtual CodeClassification RegexIdentifier
		{
			get => CodeClassification.ReSharperRegexIdentifier;
		}
		
		public virtual CodeClassification RegexMatchedSelection
		{
			get => CodeClassification.ReSharperRegexMatchedSelection;
		}

		public virtual CodeClassification RegexMatchedValue
		{
			get => CodeClassification.ReSharperRegexMatchedValue;
		}

		public virtual CodeClassification RegexSet
		{
			get => CodeClassification.ReSharperRegexSet;
		}

		public virtual CodeClassification String
		{
			get => CodeClassification.StringVerbatim;
		}

		public virtual CodeClassification StringFormatItem
		{
			get => CodeClassification.ReSharperFormatStringItem;
		}

		public virtual CodeClassification StringVerbatim
		{
			get => CodeClassification.StringVerbatim;
		}

		public virtual CodeClassification StringEscapeCharacter1
		{
			get => CodeClassification.ReSharperStringEscapeCharacter1;
		}

		public virtual CodeClassification StringEscapeCharacter2
		{
			get => CodeClassification.ReSharperStringEscapeCharacter1;
		}

	//	public virtual CodeClassification TodoItem { get => CodeClassification.}

	}
}