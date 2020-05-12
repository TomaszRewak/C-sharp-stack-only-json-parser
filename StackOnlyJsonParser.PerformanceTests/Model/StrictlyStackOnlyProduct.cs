using System;

namespace StackOnlyJsonParser.PerformanceTests.Model
{
	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlyProduct
	{
		public StackOnlyJsonString Name { get; }
		public DateTime ProductionDate { get; }
		public StackOnlySize BoxSize { get; }
		public int AvailableItems { get; }
		public StackOnlyColors Colors { get; }
		public StackOnlyRegions Regions { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlySize
	{
		public double Width { get; }
		public double Height { get; }
		public double Depth { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct StackOnlyPrice
	{
		public StackOnlyJsonString Currency { get; }
		public decimal Value { get; }
	}

	[StackOnlyJsonArray(typeof(StackOnlyJsonString))]
	internal readonly ref partial struct StackOnlyColors
	{ }

	[StackOnlyJsonDictionary(typeof(StackOnlyJsonString), typeof(StackOnlyPrice))]
	internal readonly ref partial struct StackOnlyRegions
	{ }
}
