using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonType
	{
		public string Accesibility { get; }
		public string Namespace { get; }
		public string TypeName { get; }
		public IReadOnlyCollection<JsonField> Fields { get; }

		public JsonType(INamedTypeSymbol type)
		{
			Accesibility = type.DeclaredAccessibility.ToString().ToLower();
			Namespace = type.GetNamespace();
			TypeName = type.Name;
			Fields = type
				.GetMembers()
				.OfType<IPropertySymbol>()
				.Where(SyntaxHelper.IsAutoProperty)
				.Select(field => new JsonField(field))
				.ToArray();
		}
	}
}
