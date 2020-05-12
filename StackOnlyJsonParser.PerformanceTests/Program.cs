using System;
using System.Linq;
using System.Text;

namespace StackOnlyJsonParser.PerformanceTests
{
	class Program
	{
		static void Main(string[] args)
		{
			var dataGenerator = new DataGenerator();
			var data = dataGenerator.Generate(3);

			Console.WriteLine(Encoding.UTF8.GetString(data));
		}
	}
}
