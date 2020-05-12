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
				case "System.Boolean":
				case "System.Byte":
				case "System.DateTime":
				case "System.DateTimeOffset":
				case "System.Decimal":
				case "System.Double":
				case "System.Guid":
				case "System.Int16":
				case "System.Int32":
				case "System.Int64":
				case "System.SByte":
				case "System.Single":
				case "System.String":
				case "System.UInt16":
				case "System.UInt32":
				case "System.UInt64":
					return $"{readerName}.Get{type.Split('.')[1]}()";
				default:
					return $"new {type}(ref {readerName})";
			}
		}
	}
}
