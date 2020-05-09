using System;

namespace StackOnlyJsonParser
{
	[AttributeUsage(AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
	public class StackOnlyJsonCollectionAttribute : Attribute
	{
		public StackOnlyJsonCollectionAttribute(params JsonCollectionKind[] collectionKind)
		{ }
	}
}
