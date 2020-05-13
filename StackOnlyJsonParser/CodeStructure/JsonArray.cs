using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonArray
	{
		public string Accesibility { get; set; }
		public string Namespace { get; }
		public string TypeName { get; }
		public JsonDataType ElementType { get; }

		public JsonArray(INamedTypeSymbol type)
		{
			var attributeData = type.GetAttribute(typeof(StackOnlyJsonArrayAttribute).FullName);

			Accesibility = type.DeclaredAccessibility.ToString().ToLower();
			Namespace = type.GetNamespace();
			TypeName = type.Name;
			ElementType = new JsonDataType((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value);
		}
	}
}
