using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXNamespaceContext
	{
		private readonly List<FXNamespace> _fxNamespaces
			= new List<FXNamespace>();


		[NotNull]
		public IReadOnlyList<FXNamespace> FXNamespaces
		{
			get => _fxNamespaces;
		}

		[NotNull]
		public IReadOnlyDictionary<string, string> FXNamespaceDeclarations
		{
			get => _fxNamespaces.ToDictionary(
				t => t.Name,
				t => t.NamespaceUri);
		}


		public FXNamespaceContext()
		{

		}


		public void AddNamespace(
			[NotNull] FXNamespace fxNamespace)
		{
			fxNamespace.IsNotNull(nameof(fxNamespace));
			_addNamespaceImpl(fxNamespace);
		}


		private void _addNamespaceImpl(
			FXNamespace fxNamespace)
		{
			if (fxNamespace == null)
				throw new NullReferenceException(
					$"Parameter {nameof(fxNamespace).SQuote()} value cannot be \'null\'.");

			var fxNameExists = FXNamespaceDeclarations
			                   .Keys
			                   .Contains(fxNamespace.Name);

			if (fxNameExists)
				throw new XmlException(
					$"Namespace declaration {nameof(fxNamespace.Name)}={fxNamespace.Name.Quote()} value already " +
					$"exists in the {nameof(_fxNamespaces).SQuote()} list in this document.");

			var fxNamespaceUriExists = FXNamespaceDeclarations
			                           .Values
			                           .Contains(fxNamespace.NamespaceUri);

			if (fxNamespaceUriExists)
				throw new XmlException(
					$"Namespace declaration uri {nameof(fxNamespace.NamespaceUri)}={fxNamespace.NamespaceUri.Quote()} " +
					$"value already exists in the {nameof(_fxNamespaces).SQuote()} list in this document.");

			_fxNamespaces.Add(fxNamespace);
		}

	}

	public class FXDocument
	{
		private readonly List<FXNamespace> _fxNamespaces
			= new List<FXNamespace>();


		[CanBeNull]
		public FXDeclaration FXDeclaration { get; private set; }

		[NotNull]
		public IReadOnlyList<FXNamespace> FXNamespaces
		{
			get => _fxNamespaces;
		}

		[NotNull]
		public IReadOnlyDictionary<string, string> FXNamespaceDeclarations
		{
			get => _fxNamespaces.ToDictionary(
				t => t.Name,
				t => t.NamespaceUri);
		}

		[NotNull]
		public string Name { get; }


		private FXDocument(
			[NotNull] string name)
		{
			Name = name.EnforceNotNull(nameof(name));
		}

		private FXDocument(
			[NotNull] string name,
			[NotNull, ItemNotNull] params FXNamespace[] fxNamespaces)
				: this(name)
		{
			foreach (var fxNamespace in fxNamespaces)
			{
				_addNamespaceImpl(fxNamespace);
			}
		}

		
		public static FXDocument Declare(
			[NotNull] string name)
		{
			return new FXDocument(name);
		}

		public static FXDocument Declare(
			[NotNull] string name,
			[NotNull, ItemNotNull] params FXNamespace[] fxNamespaces)
		{
			return new FXDocument(name, fxNamespaces);
		}

		public FXDocument WithDeclaration(
			[NotNull] FXDeclaration fxDeclaration)
		{
			fxDeclaration.IsNotNull(nameof(fxDeclaration));
			
			if (FXDeclaration != null)
				throw new XmlException(
					$"Could not apply {typeof(FXDeclaration).FormatName().SQuote()} from parameter " +
					$"{nameof(fxDeclaration)} because this document has already been assigned an " +
					$"{typeof(FXDocument).FormatName().SQuote()} value.");

			FXDeclaration = fxDeclaration;
			return this;
		}

		public FXDocument WithNamespace(
			[NotNull] FXNamespace fxNamespace)
		{
			fxNamespace.IsNotNull(nameof(fxNamespace));
			_addNamespaceImpl(fxNamespace);

			return this;
		}
		
		public FXDocument CreateSubNode(
			Expression<Action<FXSubNodeCreator>> fxSubNodeCreator)
		{
			return null;
		}
		
		private void _addNamespaceImpl(
			FXNamespace fxNamespace)
		{
			if (fxNamespace == null)
				throw new NullReferenceException(
					$"Parameter {nameof(fxNamespace).SQuote()} value cannot be \'null\'.");

			var fxNameExists = FXNamespaceDeclarations
			                   .Keys
			                   .Contains(fxNamespace.Name);

			if (fxNameExists)
				throw new XmlException(
					$"Namespace declaration {nameof(fxNamespace.Name)}={fxNamespace.Name.Quote()} value already " +
					$"exists in the {nameof(_fxNamespaces).SQuote()} list in this document.");

			var fxNamespaceUriExists = FXNamespaceDeclarations
			                           .Values
			                           .Contains(fxNamespace.NamespaceUri);

			if (fxNamespaceUriExists)
				throw new XmlException(
					$"Namespace declaration uri {nameof(fxNamespace.NamespaceUri)}={fxNamespace.NamespaceUri.Quote()} " +
					$"value already exists in the {nameof(_fxNamespaces).SQuote()} list in this document.");

			_fxNamespaces.Add(fxNamespace);
		}
	}
}