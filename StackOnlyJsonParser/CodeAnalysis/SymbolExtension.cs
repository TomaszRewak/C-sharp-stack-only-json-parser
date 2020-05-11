using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeAnalysis
{
	internal static class SymbolExtension
	{
		public static IEnumerable<AttributeData> GetAttributes(this ITypeSymbol type, INamedTypeSymbol attributeType)
		{
			return type
				.GetAttributes()
				.Where(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeType));
		}

		public static bool HasAttribute(this ITypeSymbol type, INamedTypeSymbol attributeType)
		{
			return type
				.GetAttributes(attributeType)
				.Any();
		}

		public static AttributeData GetAttribute(this ITypeSymbol type, INamedTypeSymbol attributeType)
		{
			return type
				.GetAttributes(attributeType)
				.First();
		}

		public static string GetFullName(this ITypeSymbol symbol)
		{
			return $"{symbol.GetNamespace()}.{symbol.Name}";
		}

		public static string GetNamespace(this ISymbol symbol)
		{
			var namespaces = new List<INamespaceSymbol>();

			for (var currentNamespace = symbol.ContainingNamespace; !currentNamespace.IsGlobalNamespace; currentNamespace = currentNamespace.ContainingNamespace)
				namespaces.Add(currentNamespace);

			return string.Join(".", namespaces.Select(n => n.Name).Reverse());
		}
	}
}
