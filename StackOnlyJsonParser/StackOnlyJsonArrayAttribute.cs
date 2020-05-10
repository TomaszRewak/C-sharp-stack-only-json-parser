using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class StackOnlyJsonArrayAttribute : Attribute
	{
		public StackOnlyJsonArrayAttribute(Type elementType)
		{ }
	}
}
