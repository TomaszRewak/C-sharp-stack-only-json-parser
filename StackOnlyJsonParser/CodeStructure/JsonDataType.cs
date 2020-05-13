using Microsoft.CodeAnalysis;
using StackOnlyJsonParser.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeStructure
{
	internal sealed class JsonDataType
	{
		public string FullName { get; }
		public bool Nullable { get; }

		public JsonDataType(ITypeSymbol symbol)
		{
			FullName = symbol.GetFullName();

			if (symbol is INamedTypeSymbol namedSymbol && FullName == typeof(Nullable).FullName)
			{
				FullName = namedSymbol.TypeArguments[0].GetFullName();
				Nullable = true;
			}
		}

		public override string ToString()
		{
			return Nullable
				? $"{FullName}?"
				: FullName;
		}
	}
}
