using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.UnitTests
{
	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlyType
	{
		public double Double { get; }
		public int Int { get; }
		public string String { get; }
		public double Multiplied => Double * Int;
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct NullableStackOnlyType
	{
		public double? Double { get; }
		public int? Int { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlyTypeWithCustomNames
	{
		[StackOnlyJsonField("double-value", "double_value")]
		public double Double { get; }
		[StackOnlyJsonField("int-value")]
		public int Int { get; }
		[StackOnlyJsonField]
		public string String { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct NestingStackOnlyType
	{
		public StackOnlyType Value1 { get; }
		public StackOnlyType Value2 { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct EmptyStackOnlyType
	{ }

	[StackOnlyJsonArray(typeof(int))]
	internal readonly ref partial struct StackOnlyIntArray
	{ }

	[StackOnlyJsonArray(typeof(int?))]
	internal readonly ref partial struct StackOnlyNullableIntArray
	{ }

	[StackOnlyJsonArray(typeof(StackOnlyType))]
	internal readonly ref partial struct StackOnlyTypeArray
	{ }

	[StackOnlyJsonDictionary(typeof(string), typeof(StackOnlyType))]
	internal readonly ref partial struct StackOnlyDictionary
	{ }

	[StackOnlyJsonArray(typeof(StackOnlyTypeArray))]
	internal readonly ref partial struct StackOnlyNestedArray
	{ }

	[StackOnlyJsonDictionary(typeof(StackOnlyJsonString), typeof(int))]
	internal readonly ref partial struct StrictlyStackOnlyDictionary
	{ }

	[StackOnlyJsonType]
	internal readonly ref partial struct RecursiveStackOnlyType
	{
		public int Id { get; }
		public RecursiveStackOnlyLazyLoader Internal { get; }
	}

	[StackOnlyJsonLazyLoader(typeof(RecursiveStackOnlyType))]
	internal readonly ref partial struct RecursiveStackOnlyLazyLoader
	{ }
}
