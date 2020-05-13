using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonDictionary
	{
		public string Accesibility { get; set; }
		public string Namespace { get; }
		public string TypeName { get; }
		public JsonDataType KeyType { get; }
		public JsonDataType ValueType { get; }

		public JsonDictionary(INamedTypeSymbol type)
		{
			var attributeData = type.GetAttribute(typeof(StackOnlyJsonDictionaryAttribute).FullName);

			Accesibility = type.DeclaredAccessibility.ToString().ToLower();
			Namespace = type.GetNamespace();
			TypeName = type.Name;
			KeyType = new JsonDataType((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value);
			ValueType = new JsonDataType((INamedTypeSymbol)attributeData.ConstructorArguments[1].Value);
		}
	}
}
