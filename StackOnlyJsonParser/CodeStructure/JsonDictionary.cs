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
		public string KeyType { get; }
		public string ValueType { get; }

		public JsonDictionary(INamedTypeSymbol type)
		{
			var attributeData = type.GetAttribute(typeof(StackOnlyJsonDictionaryAttribute).FullName);

			Accesibility = type.DeclaredAccessibility.ToString().ToLower();
			Namespace = type.GetNamespace();
			TypeName = type.Name;
			KeyType = ((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value).GetFullName();
			ValueType = ((INamedTypeSymbol)attributeData.ConstructorArguments[1].Value).GetFullName();
		}
	}
}
