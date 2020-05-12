using System;

namespace StackOnlyJsonParser.PerformanceTests.Model
{
	[StackOnlyJsonType]
	internal readonly ref partial struct StackProduct
	{
		public string Name { get; }
		public DateTime ProductionDate { get; }
		public StackSize BoxSize { get; }
		public int AvailableItems { get; }
		public StackColors Colors { get; }
		public StackRegions Regions { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct StackSize
	{
		public double Width { get; }
		public double Height { get; }
		public double Depth { get; }
	}

	[StackOnlyJsonType]
	internal readonly ref partial struct StackPrice
	{
		public string Currency { get; }
		public decimal Value { get; }
	}

	[StackOnlyJsonArray(typeof(string))]
	internal readonly ref partial struct StackColors
	{ }

	[StackOnlyJsonDictionary(typeof(string), typeof(StackPrice))]
	internal readonly ref partial struct StackRegions
	{ }

	[StackOnlyJsonArray(typeof(StackProduct))]
	internal readonly ref partial struct StackProducts
	{ }
}
