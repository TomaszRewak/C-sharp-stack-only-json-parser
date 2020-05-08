using StackOnlyJsonParser.CodeStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackOnlyJsonParser.CodeGeneration
{
	internal static class TypeGenerator
	{
		public static string Generate(JsonType type)
		{
			return @$"
using System;
using System.Buffers;
using System.Text.Json;

namespace {type.Namespace}
{{
	readonly ref struct {type.TypeName}
	{{
		public {type.TypeName}(ReadOnlySpan<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		public {type.TypeName}(ReadOnlySequence<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		private {type.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{}}
		internal {type.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{
{Indent}
		}}
	}}
}}
";
		}
	}
}
