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
{CodeGenetaionHelper.Indent(2, GenerateFields(type))}

		public {type.TypeName}(ReadOnlySpan<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		public {type.TypeName}(ReadOnlySequence<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		private {type.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{}}
		internal {type.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{
{CodeGenetaionHelper.Indent(3, GenerateConstructor(type))}
		}}
	}}
}}
";
		}

		private static string GenerateFields(JsonType type)
		{
			return CodeGenetaionHelper.JoinLines(type.Fields.Select(GenerateField));
		}

		private static string GenerateField(JsonField field)
		{
			return @$"public readonly {field.Type.FullName} {field.Name};";
		}

		private static string GenerateConstructor(JsonType type)
		{
			return @$"
{GenerateFieldInitializers(type)}

if (jsonReader.TokenType == JsonTokenType.None) jsonReader.Read();
if (jsonReader.TokenType != JsonTokenType.StartObject) throw new JsonException(""Expected '{{'"");

while (jsonReader.TokenType != JsonTokenType.EndObject)
{{
	if (!jsonReader.Read()) throw new JsonException(""Expected '}}'"");
	if (jsonReader.TokenType != JsonTokenType.PropertyName) throw new JsonException(""Expected property name"");

	if (jsonReader.ValueTextEquals(""PropertyName""))
	{{
		jsonReader.Read();
		A = jsonReader.GetDouble();
	}}
	else if (jsonReader.ValueTextEquals(""PropertyName""))
	{{
		jsonReader.Read();
		B = jsonReader.GetDouble();
	}}
	else
	{{
		jsonReader.Read();
		jsonReader.Skip();
	}}

	jsonReader.Read();
}}
";
		}

		private static string GenerateFieldInitializers(JsonType type)
		{
			return CodeGenetaionHelper.JoinLines(type.Fields.Select(GenerateFieldInitializer));
		}

		private static string GenerateFieldInitializer(JsonField field)
		{
			return $"{field.Name} = default;";
		}
	}
}
