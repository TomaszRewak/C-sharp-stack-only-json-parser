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
		public string ElementType { get; }

		public JsonArray(string accesibility, string @namespace, string typeName, string elementType)
		{
			Accesibility = accesibility;
			Namespace = @namespace;
			TypeName = typeName;
			ElementType = elementType;
		}
	}
}
