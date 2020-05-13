using Newtonsoft.Json;
using StackOnlyJsonParser.PerformanceTests.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StackOnlyJsonParser.PerformanceTests
{
	class Program
	{
		private static Stopwatch stopwatch = new Stopwatch();
		private static decimal price;

		static void Main(string[] args)
		{
			var dataGenerator = new DataGenerator();
			var data = dataGenerator.Generate(int.Parse(args[1]));
			var stringData = Encoding.UTF8.GetString(data);

			for (int i = 0; i < 5; i++)
			{
				GC.Collect();
				var allocatedBytes = GC.GetAllocatedBytesForCurrentThread();
				var usedBytes = GC.GetTotalMemory(false);

				price = 0;

				switch (args[0])
				{
					case "Newtonsoft":
						TestHeap1(stringData);
						break;
					case "System.Text.Json":
						TestHeap2(data);
						break;
					case "Stack":
						TestStack(data);
						break;
					case "StackOnly":
						TestStackOnly(data);
						break;
				}

				allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - allocatedBytes;
				usedBytes = GC.GetTotalMemory(false) - usedBytes;

				Console.WriteLine($"{price}\t{args[0]}\t{args[1]}\t{stopwatch.ElapsedMilliseconds}\t{allocatedBytes}\t{usedBytes}");
			}
		}

		static void TestHeap1(string data)
		{
			stopwatch.Restart();
			var products = JsonConvert.DeserializeObject<List<HeapProduct>>(data);

			foreach (var product in products)
				foreach (var color in product.Colors)
					if (color == "red")
						foreach (var region in product.Regions)
							if (region.Key == "PL")
								price += product.AvailableItems * region.Value.Value;

			stopwatch.Stop();
		}

		static void TestHeap2(ReadOnlySpan<byte> data)
		{
			stopwatch.Restart();
			var products = JsonSerializer.Deserialize<List<HeapProduct>>(data);

			foreach (var product in products)
				foreach (var color in product.Colors)
					if (color == "red")
						foreach (var region in product.Regions)
							if (region.Key == "PL")
								price += product.AvailableItems * region.Value.Value;

			stopwatch.Stop();
		}

		static void TestStack(ReadOnlySpan<byte> data)
		{
			stopwatch.Restart();
			var products = new StackProducts(data);

			foreach (var product in products)
				foreach (var color in product.Colors)
					if (color == "red")
						foreach (var region in product.Regions)
							if (region.Key == "PL")
								price += product.AvailableItems * region.Value.Value;

			stopwatch.Stop();
		}

		static void TestStackOnly(ReadOnlySpan<byte> data)
		{
			stopwatch.Restart();
			var products = new StackOnlyProducts(data);

			foreach (var product in products)
				foreach (var color in product.Colors)
					if (color.ValueTextEquals("red"))
						foreach (var region in product.Regions)
							if (region.Key.ValueTextEquals("PL"))
								price += product.AvailableItems * region.Value.Value;

			stopwatch.Stop();
		}
	}
}
