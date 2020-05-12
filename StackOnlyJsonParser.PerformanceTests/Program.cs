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
		static void Main(string[] args)
		{
			var dataGenerator = new DataGenerator();
			var data = dataGenerator.Generate(1000000);

			Console.WriteLine("Start");

			TestHeap1(Encoding.UTF8.GetString(data));
			TestHeap2(data);
			TestStack(data);
			TestStackOnly(data);
			TestHeap1(Encoding.UTF8.GetString(data));
			TestHeap2(data);
			TestStack(data);
			TestStackOnly(data);
		}

		static void TestHeap1(string data)
		{
			GC.Collect();
			var bytes = GC.GetAllocatedBytesForCurrentThread();
			var usedBytes = GC.GetTotalMemory(false);
			var stopwatch = Stopwatch.StartNew();
			var products = JsonConvert.DeserializeObject<List<HeapProduct>>(data);
			var price = 0m;

			foreach (var product in products)
			{
				foreach (var color in product.Colors)
				{
					if (color == "red")
					{
						foreach (var region in product.Regions)
						{
							if (region.Key == "PL")
							{
								price += product.AvailableItems * region.Value.Value;
								break;
							}
						}
						break;
					}
				}
			}

			stopwatch.Stop();
			Console.WriteLine($"{price} {stopwatch.Elapsed} \t{GC.GetAllocatedBytesForCurrentThread() - bytes} \t{GC.GetTotalMemory(false) - usedBytes}");
		}

		static void TestHeap2(ReadOnlySpan<byte> data)
		{
			GC.Collect();
			var bytes = GC.GetAllocatedBytesForCurrentThread();
			var usedBytes = GC.GetTotalMemory(false);
			var stopwatch = Stopwatch.StartNew();
			var products = JsonSerializer.Deserialize<List<HeapProduct>>(data);
			var price = 0m;

			foreach (var product in products)
			{
				foreach (var color in product.Colors)
				{
					if (color == "red")
					{
						foreach (var region in product.Regions)
						{
							if (region.Key == "PL")
							{
								price += product.AvailableItems * region.Value.Value;
								break;
							}
						}
						break;
					}
				}
			}

			stopwatch.Stop();
			Console.WriteLine($"{price} {stopwatch.Elapsed} \t{GC.GetAllocatedBytesForCurrentThread() - bytes} \t{GC.GetTotalMemory(false) - usedBytes}");
		}

		static void TestStack(ReadOnlySpan<byte> data)
		{
			GC.Collect();
			var bytes = GC.GetAllocatedBytesForCurrentThread();
			var usedBytes = GC.GetTotalMemory(false);
			var stopwatch = Stopwatch.StartNew();
			var products = new StackProducts(data);
			var price = 0m;

			foreach (var product in products)
			{
				foreach (var color in product.Colors)
				{
					if (color == "red")
					{
						foreach (var region in product.Regions)
						{
							if (region.Key == "PL")
							{
								price += product.AvailableItems * region.Value.Value;
								break;
							}
						}
						break;
					}
				}
			}

			stopwatch.Stop();
			Console.WriteLine($"{price} {stopwatch.Elapsed} \t{GC.GetAllocatedBytesForCurrentThread() - bytes} \t{GC.GetTotalMemory(false) - usedBytes}");
		}

		static void TestStackOnly(ReadOnlySpan<byte> data)
		{
			GC.Collect();
			var bytes = GC.GetAllocatedBytesForCurrentThread();
			var usedBytes = GC.GetTotalMemory(false);
			var stopwatch = Stopwatch.StartNew();
			var products = new StackOnlyProducts(data);
			var price = 0m;

			foreach (var product in products)
			{
				foreach (var color in product.Colors)
				{
					if (color.ValueTextEquals("red"))
					{
						foreach (var region in product.Regions)
						{
							if (region.Key.ValueTextEquals("PL"))
							{
								price += product.AvailableItems * region.Value.Value;
								break;
							}
						}
						break;
					}
				}
			}

			stopwatch.Stop();
			Console.WriteLine($"{price} {stopwatch.Elapsed} \t{GC.GetAllocatedBytesForCurrentThread() - bytes} \t{GC.GetTotalMemory(false) - usedBytes}");
		}
	}
}
