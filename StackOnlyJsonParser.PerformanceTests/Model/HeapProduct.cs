using System;
using System.Collections.Generic;

namespace StackOnlyJsonParser.PerformanceTests.Model
{
	internal class HeapProduct
	{
		public string Name { get; set; }
		public DateTime ProductionDate { get; set; }
		public HeapSize BoxSize { get; set; }
		public int AvailableItems { get; set; }
		public List<string> Colors { get; set; }
		public Dictionary<string, HeapPrice> Regions { get; set; }
	}

	internal class HeapSize
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public double Depth { get; set; }
	}

	internal class HeapPrice
	{
		public string Currency { get; set; }
		public decimal Value { get; set; }
	}
}
