using System;
using System.Collections.Generic;

namespace StackOnlyJsonParser.PerformanceTests.Model
{
	public class HeapProduct
	{
		public string Name { get; set; }
		public DateTime ProductionDate { get; set; }
		public HeapSize BoxSize { get; set; }
		public int AvailableItems { get; set; }
		public List<string> Colors { get; set; }
		public Dictionary<string, HeapPrice> Regions { get; set; }
	}

	public class HeapSize
	{
		public double Width { get; set; }
		public double Height { get; set; }
		public double Depth { get; set; }
	}

	public class HeapPrice
	{
		public string Currency { get; set; }
		public decimal Value { get; set; }
	}
}
