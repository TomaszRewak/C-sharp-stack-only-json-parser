using StackOnlyJsonParser.CodeStructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class LazyLoaderGenerator
	{
		public static string Generate(JsonLazyLoader lazyLoader)
		{
			return $@"
using System;
using System.Buffers;
using System.Text.Json;

namespace {lazyLoader.Namespace}
{{
	{lazyLoader.Accesibility} readonly ref partial struct {lazyLoader.TypeName}
	{{
		private readonly Utf8JsonReader _jsonReader;

		public readonly bool HasValue {{ get; }}

		public {lazyLoader.TypeName}(ref Utf8JsonReader jsonReader)
		{{
			HasValue = true;
			_jsonReader = jsonReader;

			jsonReader.Skip();
		}}

		public {lazyLoader.LazyType.FullName} Load()
		{{
			var jsonReader = _jsonReader;

			return {DeserializationGenerator.Generate("jsonReader", lazyLoader.LazyType)};
		}}
	}}
}}
";
		}
	}
}
