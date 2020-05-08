using System;

namespace StackOnlyJsonParser
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class StackOnlyJsonCollectionAttribute : Attribute
	{
		public StackOnlyJsonCollectionAttribute(params JsonCollectionKind[] collectionKind)
		{ }
	}
}
