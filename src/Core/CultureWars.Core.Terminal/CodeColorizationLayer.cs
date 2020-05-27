namespace CultureWars
{
	public enum ComplexCodeKind
	{
		PlainText,
		Brace,
		Comment,
		NonStaticClassIdentifier,
		StaticClassIdentifier,
		ConstantIdentifier,
		DelegateIdentifier,
		EnumIdentifier,
		EnumMemberIdentifier,
		EventIdentifier,
		ExtensionMethodIdentifier,
		FieldIdentifier,
		InterfaceIdentifier,
		LocalMethodIdentifier,
		MethodIdentifier,
		NamespaceIdentifier,
		ParameterIdentifier,
		PathIdentifier,
		PropertyIdentifier,
		TypeParameterIdentifier,
		ValueTupleComponentIdentifier,
		VariableLocalImmutableIdentifier,
		MutableLocalVariableIdentifier,
		Keyword,
		Number,
		Operator,
		RegexComment,
		RegexGroup,
		RegexIdentifier,
		RegexMatchedSelection,
		RegexMatchedValue,
		RegexSet,
		String,
		StringFormatItem,
		StringVerbatim,
		StringEscapeCharacter1,
		StringEscapeCharacter2,
	}

	public enum CodeKind
	{
		BraceOperator = 1,
		ClassIdentifier = 2,
		ConstantIdentifier = 3,
		DelegateIdentifier = 4,
		EnumIdentifier = 5,
		EventIdentifier = 6,
		FieldIdentifier = 7,
		InterfaceIdentifier = 8,
		MethodIdentifier = 9,
		NamespaceIdentifier = 10,
		ParameterIdentifier = 11,
		PropertyIdentifier = 12,
		TypeParameterIdentifier = 13,
		Keyword = 14,
		Number = 15,
		String = 16,
	}

	public abstract class CodeColorizationLayer
	{
		public abstract string LayerName { get; }
	}
}