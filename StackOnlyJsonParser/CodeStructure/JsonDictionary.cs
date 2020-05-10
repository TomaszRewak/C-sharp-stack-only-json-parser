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

		public JsonDictionary(string accesibility, string @namespace, string typeName, string keyType, string valueType)
		{
			Accesibility = accesibility;
			Namespace = @namespace;
			TypeName = typeName;
			KeyType = keyType;
			ValueType = valueType;
		}
	}
}
