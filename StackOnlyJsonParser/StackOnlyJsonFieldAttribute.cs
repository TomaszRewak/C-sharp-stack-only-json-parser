using System;

namespace StackOnlyJsonParser
{
	public class StackOnlyJsonFieldAttribute : Attribute
	{
		public StackOnlyJsonFieldAttribute(params string[] serializedNames)
		{ }
	}
}
