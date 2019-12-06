using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXElementBuilder
	{
		private FXElement _fxElement;
		private string _name;
		private object _simpleChildContent;

		private readonly List<Func<FXElementBuilder, FXElementBuilder>> _complexChildNodeBuilders
			= new List<Func<FXElementBuilder, FXElementBuilder>>();


		public FXElementBuilder()
		{
		}


		public FXElementBuilder WithComplexChildNode(
			string name,
			Func<FXElementBuilder, FXElementBuilder> childBuilder)
		{
			_name = name;
			//childBuilder.Invoke(this);
			_complexChildNodeBuilders.Add(childBuilder);
			return this;
		}

		public FXElementBuilder WithSimpleChildNode(
			string name,
			object value)
		{
			_name = name;
			_simpleChildContent = value;
			return this;
		}


		internal FXElement Build()
		{
			if (_complexChildNodeBuilders.Any())
			{
				var complexElements = _complexChildNodeBuilders
					.Select(t => t.Invoke(this).Build());

				_fxElement = FXElement.Get(_name, complexElements);
			}
			else
			{
				_fxElement = FXElement.Get(_name, _simpleChildContent);
			}

			return _fxElement;
		}

		public static implicit operator FXElement(
			FXElementBuilder @this)
		{
			return @this.Build();
		}
	}

	/*				foreach (var complexChildNodeBuilder in _complexChildNodeBuilders)
				{
					var s = complexChildNodeBuilder.Invoke(this);
				}*/
	public class FXElement
	{
		[CanBeNull]
		public FXNamespace FXNamespace { get; }

		[NotNull]
		public string Name { get; }

		[CanBeNull]
		public object Value { get; }


		public static FXElementBuilder Builder
		{
			get => new FXElementBuilder();
		}


		private FXElement(
			[NotNull] string name,
			[CanBeNull] object value)
		{
			Name = name.EnforceNotNull(nameof(name));
			Value = value;
		}

		private FXElement(
			[NotNull] FXNamespace fxNamespace,
			[NotNull] string name,
			[CanBeNull] object value)
			: this(name, value)
		{
			FXNamespace = fxNamespace.EnforceNotNull(nameof(fxNamespace));
		}


		public static FXElement Get(
			[NotNull] string name,
			[CanBeNull] object value)
		{
			return new FXElement(
				name,
				value);
		}

		public static FXElement Get(
			[NotNull] FXNamespace fxNamespace,
			[NotNull] string name,
			[CanBeNull] object value)
		{
			return new FXElement(
				fxNamespace,
				name,
				value);
		}

		public static implicit operator FXElement(
			(string name, object value) @this)
		{
			var (name, value) = @this;
			return Get(name, value);
		}
	}
}