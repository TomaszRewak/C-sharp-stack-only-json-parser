using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class CodeGenetaionHelper
	{
		public static string Indent(int tabs, string text)
		{
			var lines = text
				.Split('\n')
				.Select(line => IndentLine(tabs, line));

			return string.Join('\n', lines);
		}

		private static string IndentLine(int tabs, string line)
		{
			Debug.Assert(!line.Contains('\n'));

			return $"{new string('\t', tabs)}{line}";
		}
	}
}
