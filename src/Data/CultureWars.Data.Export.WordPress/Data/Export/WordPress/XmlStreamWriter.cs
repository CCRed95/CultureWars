using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using CultureWars.Data.Export.WordPress.XmlWriter;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress
{
	public class ConsoleStreamWriter
		: StreamWriter
	{
		internal class XmlFragment
		{

		}

		internal class XmlNamespaceDeclarationFragment
		{


		}
		internal class XmlElementFragment
		{

			public string LocalName { get; }

		}

		internal class RootXmlElementFragment
		{
			public IReadOnlyList<XmlNamespaceDeclarationFragment> NamespaceFragments { get; }
		}


		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] Stream stream)
			: base(stream)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] Stream stream, [NotNull] Encoding encoding)
			: base(stream, encoding)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] Stream stream, [NotNull] Encoding encoding, int bufferSize)
			: base(stream, encoding, bufferSize)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] Stream stream, [NotNull] Encoding encoding, int bufferSize, bool leaveOpen)
			: base(stream, encoding, bufferSize, leaveOpen)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] string path)
			: base(path)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] string path, bool append)
			: base(path, append)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] string path, bool append, [NotNull] Encoding encoding)
			: base(path, append, encoding)
		{
		}

		/// <inheritdoc />
		public ConsoleStreamWriter([NotNull] string path, bool append, [NotNull] Encoding encoding, int bufferSize)
			: base(path, append, encoding, bufferSize)
		{
		}


		/// <inheritdoc />
		public override void WriteLine(ReadOnlySpan<char> buffer)
		{
			base.WriteLine(buffer);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(buffer.ToString());
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <inheritdoc />
		public override void WriteLine(string value)
		{
			base.WriteLine(value);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(value);
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <inheritdoc />
		public override void WriteLine()
		{
			base.WriteLine();
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <inheritdoc />
		public override void Write(string value)
		{
			base.Write(value);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(value);
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <inheritdoc />
		public override void Write(int value)
		{
			base.Write(value);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(value);
			Console.ForegroundColor = ConsoleColor.White;
		}

		/// <inheritdoc />
		public override void WriteLine(int value)
		{
			base.WriteLine(value);
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.Write(value);
			Console.ForegroundColor = ConsoleColor.White;
		}
	}


	public class XmlStreamWriter
	{
		protected readonly ConsoleStreamWriter _streamWriter;
		private readonly List<FXNamespace> _fxNamespaces
			= new List<FXNamespace>();
		private readonly Stack<FXElement> _openElements
			= new Stack<FXElement>();


		/// <summary>
		/// The <see cref="FXDeclaration"/> that has been applied to this document configuration.
		/// This value can be <see langword="null"/> if it has not been configured.
		/// </summary>
		[CanBeNull]
		public FXDeclaration FXDeclaration { get; private set; }

		/// <summary>
		/// A <see cref="IReadOnlyList{T}"/> of <see cref="FXNamespace"/> declarations that
		/// have been applied to this document configuration.
		/// </summary>
		[NotNull]
		public IReadOnlyList<FXNamespace> FXNamespaces
		{
			get => _fxNamespaces;
		}

		/// <summary>
		/// A <see cref="IReadOnlyDictionary{TKey,TValue}"/> of mappings between of registered
		///	namespace prefixes, and their associated namespace Uris.
		/// </summary>
		[NotNull]
		public IReadOnlyDictionary<string, string> FXNamespaceDeclarations
		{
			get => _fxNamespaces.ToDictionary(
				t => t.Name,
				t => t.NamespaceUri);
		}


		/// <summary>
		/// The constructor for the <see cref="XmlStreamWriter"/> class.
		/// </summary>
		/// <param name="streamWriter">
		/// A <see cref="StreamWriter"/> instance to provide the base file texxt writer.
		/// </param>
		public XmlStreamWriter(
			[NotNull] ConsoleStreamWriter streamWriter)
		{
			_streamWriter = streamWriter.EnforceNotNull(nameof(streamWriter));
		}


		/// <summary>
		/// Provides a document-configuration providing a <see cref="FXDeclaration"/>.
		/// </summary>
		/// <param name="fxDeclaration">
		/// The <see cref="FXDeclaration"/> instance to apply to this document configuration.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WithDeclaration(
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

		/// <summary>
		/// Adds a <see cref="FXNamespace"/> declaration to the document configuration.
		/// </summary>
		/// <param name="fxNamespace">
		///	The <see cref="FXDeclaration"/> instance to add to this document configuration.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WithNamespace(
			[NotNull] FXNamespace fxNamespace)
		{
			fxNamespace.IsNotNull(nameof(fxNamespace));
			_addNamespaceImpl(fxNamespace);
			return this;
		}


		#region WriteStartElement
		public XmlStreamWriter WriteStartElement(
			[NotNull] string localName,
			params (string attribute, string value)[] attributes)
		{
			return _writeStartElementImpl(
				null,
				localName);
		}

		public XmlStreamWriter WriteStartElement(
			[NotNull] string localName)
		{
			return _writeStartElementImpl(
				null,
				localName);
		}

		public XmlStreamWriter WriteStartElement(
			[NotNull] FXNsRef fxNsRef,
			[NotNull] string localName)
		{
			var resolvedNamespace = fxNsRef.ResolveNamespace(_fxNamespaces);

			return _writeStartElementImpl(
				resolvedNamespace,
				localName);
		}

		/// <summary>
		/// Writes a start element with the local name <paramref name="localName"/>, and in the
		///	provided <paramref name="fxNamespace"/>, if applicable.
		/// </summary>
		/// <param name="fxNamespace">
		/// The <see cref="FXNamespace"/> declaration that the resulting <see cref="FXElement"/>
		/// is declared in. This value can be null for elements with no namespace.
		/// </param>
		/// <param name="localName">
		/// The local name of the element type to create.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteStartElement(
			[NotNull] FXNamespace fxNamespace,
			[NotNull] string localName)
		{
			return _writeStartElementImpl(
				fxNamespace,
				localName);
		}

		private XmlStreamWriter _writeStartElementImpl(
			[CanBeNull] FXNamespace fxNamespace,
			[NotNull] string localName,
			params (string attribute, string value)[] attributes)
		{
			var isRootElement = _openElements.Count == 0;
			var xmlnsIndent = _openElements.Count * 2;

			FXElement element;
			if (fxNamespace != null)
				element = FXElement.Get(
					fxNamespace,
					localName,
					null);
			else
			{
				element = FXElement.Get(
					localName,
					null);
			}

			WriteIndent(_openElements.Count);

			_openElements.Push(element);
			xmlnsIndent++;

			_streamWriter.Write("<");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");

				xmlnsIndent += element.FXNamespace.Name.Length + 1;
			}
			xmlnsIndent += element.Name.Length;

			_streamWriter.Write(element.Name);

			if (attributes != null)
			{
				foreach (var attribute in attributes)
				{
					_streamWriter.Write(" ");
					_streamWriter.Write(attribute.attribute);
					_streamWriter.Write("=");
					_streamWriter.Write(attribute.value.Quote());
				}
			}

			if (!isRootElement)
				_streamWriter.WriteLine(">");
			else
				_streamWriter.Write(" ");

			xmlnsIndent++;

			if (!isRootElement)
				return this;

			var nsIndex = 0;
			foreach (var documentNamespace in FXNamespaces)
			{
				if (nsIndex > 0)
					WriteWhitespace(xmlnsIndent);

				_streamWriter.Write("xmlns");
				_streamWriter.Write(":");
				_streamWriter.Write(documentNamespace.Name);
				_streamWriter.Write("=");
				_streamWriter.Write(documentNamespace.NamespaceUri.Quote());

				if (nsIndex >= FXNamespaces.Count - 1)
					_streamWriter.WriteLine(">");
				else
					_streamWriter.WriteLine();

				nsIndex++;
			}
			return this;
		}

		#endregion


		#region WriteInlineElement
		public XmlStreamWriter WriteInlineElement(
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			return _writeInlineElementImpl(
				null,
				localName,
				value);
		}

		public XmlStreamWriter WriteInlineElement(
			[NotNull] FXNsRef fxNsRef,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			var resolvedNamespace = fxNsRef.ResolveNamespace(_fxNamespaces);

			return _writeInlineElementImpl(
				resolvedNamespace,
				localName,
				value);
		}

		public XmlStreamWriter WriteInlineElement(
			[NotNull] FXNamespace fxNamespace,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			return _writeInlineElementImpl(
				fxNamespace,
				localName,
				value);
		}

		/// <summary>
		/// Writes a simple inline element with the local name <paramref name="localName"/>, and in the
		///	provided <paramref name="fxNamespace"/>, if applicable.
		/// </summary>
		/// <param name="fxNamespace">
		/// The <see cref="FXNamespace"/> declaration that the resulting <see cref="FXElement"/>
		/// is declared in. This value can be null for elements with no namespace.
		/// </param>
		/// <param name="localName">
		/// The local name of the element type to create.
		/// </param>
		/// <param name="value">
		/// The text value of the simple inline element.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		private XmlStreamWriter _writeInlineElementImpl(
			[CanBeNull] FXNamespace fxNamespace,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			FXElement element;
			if (fxNamespace != null)
				element = FXElement.Get(
					fxNamespace,
					localName,
					null);
			else
			{
				element = FXElement.Get(
					localName,
					null);
			}

			WriteIndent(_openElements.Count);

			_openElements.Push(element);

			_streamWriter.Write("<");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");
			}
			_streamWriter.Write(element.Name);
			_streamWriter.Write(">");

			_streamWriter.Write(value);

			_streamWriter.Write("</");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");
			}
			_streamWriter.Write(element.Name);
			_streamWriter.WriteLine(">");

			_openElements.Pop();
			return this;
		}

		#endregion


		#region WriteInlineCDataElement
		public XmlStreamWriter WriteInlineCDataElement(
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			return _writeInlineCDataElementImpl(
				null,
				localName,
				value);
		}

		public XmlStreamWriter WriteInlineCDataElement(
			[NotNull] FXNsRef fxNsRef,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			var resolvedNamespace = fxNsRef.ResolveNamespace(_fxNamespaces);

			return _writeInlineCDataElementImpl(
				resolvedNamespace,
				localName,
				value);
		}

		/// <summary>
		/// Writes a simple inline element in a <see cref="FXCData"/> element with the local name
		/// <paramref name="localName"/>, and in the provided <paramref name="fxNamespace"/>, if applicable.
		/// </summary>
		/// <param name="fxNamespace">
		/// The <see cref="FXNamespace"/> declaration that the resulting <see cref="FXElement"/>
		/// is declared in.
		/// </param>
		/// <param name="localName">
		/// The local name of the element type to create.
		/// </param>
		/// <param name="value">
		/// The text value of the simple inline element's CData element.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteInlineCDataElement(
			[NotNull] FXNamespace fxNamespace,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			return _writeInlineCDataElementImpl(
				fxNamespace,
				localName,
				value);
		}

		private XmlStreamWriter _writeInlineCDataElementImpl(
			[CanBeNull] FXNamespace fxNamespace,
			[NotNull] string localName,
			[CanBeNull] string value)
		{
			FXElement element;
			if (fxNamespace != null)
				element = FXElement.Get(
					fxNamespace,
					localName,
					null);
			else
			{
				element = FXElement.Get(
					localName,
					null);
			}

			WriteIndent(_openElements.Count);

			_openElements.Push(element);

			_streamWriter.Write("<");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");
			}
			_streamWriter.Write(element.Name);
			_streamWriter.Write(">");

			if (null != value && value.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				throw new ArgumentException(
					$"Invalid CData value argument {value.Quote()}.");

			_streamWriter.Write("<![CDATA[");

			if (null != value)
				_streamWriter.Write(value);

			_streamWriter.Write("]]>");

			_streamWriter.Write("</");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");
			}
			_streamWriter.Write(element.Name);
			_streamWriter.WriteLine(">");

			_openElements.Pop();
			return this;
		}

		#endregion


		#region WriteCDataElement
		public XmlStreamWriter WriteCDataElement(
			[CanBeNull] string value)
		{
			return _writeCDataElementImpl(value);
		}

		private XmlStreamWriter _writeCDataElementImpl(
			[CanBeNull] string value)
		{
			if (null != value && value.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				throw new ArgumentException(
					$"Invalid CData value argument {value.Quote()}.");

			_streamWriter.Write("<![CDATA[");

			if (null != value)
				_streamWriter.Write(value);

			_streamWriter.Write("]]>");
			return this;
		}

		#endregion

		/// <summary>
		/// Writes the end element for the next element in the <see cref="_openElements"/>
		/// <see cref="Stack{T}"/>, and calls <see cref="Stack{T}.Pop()"/> on the element.
		/// </summary>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteEndElement()
		{
			var element = _openElements.Pop();

			WriteIndent(_openElements.Count);

			_streamWriter.Write("</");
			if (element.FXNamespace != null)
			{
				_streamWriter.Write(element.FXNamespace.Name);
				_streamWriter.Write(":");
			}
			_streamWriter.Write(element.Name);
			_streamWriter.WriteLine(">");
			return this;
		}

		/// <summary>
		/// Writes whitespace indent levels, with the provided <paramref name="level"/> of indentation.
		/// The whitespace is written as spaces, and the indent width is 2 spaces.
		/// </summary>
		/// <param name="level">
		/// The indentation level indicating the number of spaces to write based on the indent width.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteIndent(
			int level)
		{
			for (var i = 0; i < level; i++)
				_streamWriter.Write("  ");
			return this;
		}

		/// <summary>
		/// Writes whitespace using spaces, with the provided <paramref name="spaces"/> length. 
		/// </summary>
		/// <param name="spaces">
		/// The number of spaces to write.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteWhitespace(
			int spaces)
		{
			for (var i = 0; i < spaces; i++)
				_streamWriter.Write(" ");
			return this;
		}

		/// <summary>
		/// Writes a CData object with the specified <paramref name="text"/> in the format <!--<![CDATA[$text$]]>-->.
		/// </summary>
		/// <param name="text">
		/// The text of the CData object.
		/// </param>
		/// <returns>
		/// Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteCData(
			[CanBeNull] string text)
		{
			if (null != text && text.IndexOf("]]>", StringComparison.Ordinal) >= 0)
				throw new ArgumentException(
					$"Invalid CData text argument {text.Quote()}.");

			_streamWriter.Write("<![CDATA[");

			if (null != text)
				_streamWriter.Write(text);

			_streamWriter.Write("]]>");
			return this;
		}

		/// <summary>
		/// Writes an Xml comment with the specified <paramref name="text"/> in the format <!--$text$-->.
		/// </summary>
		/// <param name="text">
		/// The text of the Xml comment.
		/// </param>
		/// <returns>
		///	Returns the current instance of the <see cref="XmlStreamWriter"/> class for chained calls.
		/// </returns>
		public XmlStreamWriter WriteComment(
			string text)
		{
			if (null != text && (text.IndexOf("--", StringComparison.Ordinal) >= 0
					|| text.Length != 0
					&& text[^1] == '-'))
				throw new ArgumentException(
					$"Invalid Xml Comment text argument {text.Quote()}.");

			_streamWriter.Write("<!--");

			if (null != text)
				_streamWriter.Write(text);

			_streamWriter.Write("-->");
			return this;
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