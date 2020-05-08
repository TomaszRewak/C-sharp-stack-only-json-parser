using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class StackOnlyJsonTypeAttribute : Attribute
	{
		public StackOnlyJsonTypeAttribute(string parserTypeName = null, string parserNamespace = null)
		{ }
	}
}
