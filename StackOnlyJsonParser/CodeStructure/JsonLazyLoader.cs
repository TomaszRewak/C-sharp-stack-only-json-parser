using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonLazyLoader
	{
		public string Accesibility { get; set; }
		public string Namespace { get; }
		public string TypeName { get; }
		public JsonDataType LazyType { get; }

		public JsonLazyLoader(INamedTypeSymbol type)
		{
			var attributeData = type.GetAttribute(typeof(StackOnlyJsonLazyLoaderAttribute).FullName);

			Accesibility = type.DeclaredAccessibility.ToString().ToLower();
			Namespace = type.GetNamespace();
			TypeName = type.Name;
			LazyType = new JsonDataType((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value);
		}
	}
}
