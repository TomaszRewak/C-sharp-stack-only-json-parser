using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeAnalysis
{
	internal static class SymbolExtension
	{
		public static IEnumerable<AttributeData> GetAttributes(this ISymbol type, string typeName)
		{
			return type
				.GetAttributes()
				.Where(a => a.AttributeClass.GetFullName() == typeName);
		}

		public static bool HasAttribute(this ISymbol type, string typeName)
		{
			return type
				.GetAttributes(typeName)
				.Any();
		}

		public static AttributeData GetAttribute(this ISymbol type, string typeName)
		{
			return type
				.GetAttributes(typeName)
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
