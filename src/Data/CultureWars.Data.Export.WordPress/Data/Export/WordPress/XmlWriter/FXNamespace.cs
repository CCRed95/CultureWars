using System.Collections.Generic;
using System.Linq;
using Ccr.Std.Core.Extensions;
using CultureWars.Core.Extensions;
using JetBrains.Annotations;

namespace CultureWars.Data.Export.WordPress.XmlWriter
{
	public class FXNamespace
	{
		[NotNull]
		public string Name { get; }

		[NotNull]
		public string NamespaceUri { get; }


		private FXNamespace(
			[NotNull] string name,
			[NotNull] string namespaceUri)
		{
			Name = name.EnforceNotNull(nameof(name));
			NamespaceUri = namespaceUri.EnforceNotNull(nameof(namespaceUri));
		}


		public static FXNamespace Get(
			[NotNull] string name,
			[NotNull] string namespaceUri)
		{
			return new FXNamespace(
				name,
				namespaceUri);
		}
		

		public static implicit operator FXNamespace(
			(string name, string namespaceUri) @this)
		{
			var (name, namespaceUri) = @this;
			return Get(name, namespaceUri);
		}
	}

	public class FXNsRef
	{
		[NotNull]
		public string Name { get; }
		

		private FXNsRef(
			[NotNull] string name)
		{
			Name = name.EnforceNotNull(nameof(name));
		}


		public static FXNsRef FromName(
			[NotNull] string name)
		{
			return new FXNsRef(name);
		}


		public FXNamespace ResolveNamespace(
			IReadOnlyList<FXNamespace> nsContext)
		{
			var resolvedNamespace = nsContext
				.SingleOrDefault(t => t.Name == Name);

			if (resolvedNamespace == null)
				throw new KeyNotFoundException(
					$"Could not find a resolved namespace from the name {Name.Quote()}.");

			return resolvedNamespace;
		}
	}
}