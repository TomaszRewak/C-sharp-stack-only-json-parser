using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class DeserializationGenerator
	{
		public static string Generate(string readerName, string type)
		{
			switch (type)
			{
				case "System.Int32":
				case "System.Double":
				case "System.String":
					return $"{readerName}.Get{type.Split('.')[1]}()";
				default:
					return $"new {type}(ref {readerName})";
			}
		}
	}
}
