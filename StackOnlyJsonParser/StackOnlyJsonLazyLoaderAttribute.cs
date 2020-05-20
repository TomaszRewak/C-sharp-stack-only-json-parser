using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser
{
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class StackOnlyJsonLazyLoaderAttribute : Attribute
	{
		public StackOnlyJsonLazyLoaderAttribute(Type lazyType)
		{ }
	}
}
