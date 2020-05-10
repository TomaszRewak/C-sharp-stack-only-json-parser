using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackOnlyJsonParser.CodeAnalysis
{
	internal static class TypeSymbolExtension
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
	}
}
