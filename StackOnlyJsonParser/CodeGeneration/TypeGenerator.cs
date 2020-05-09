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
	public readonly bool HasValue;

	{type.Accesibility} readonly ref partial struct {type.TypeName}
	{{
		public {type.TypeName}(ReadOnlySpan<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		public {type.TypeName}(ReadOnlySequence<byte> jsonData) : this(new Utf8JsonReader(jsonData, new JsonReaderOptions {{ CommentHandling = JsonCommentHandling.Skip }}))
		{{}}
		private {type.TypeName}(Utf8JsonReader jsonReader) : this(ref jsonReader)
		{{}}
		internal {type.TypeName}(ref Utf8JsonReader jsonReader)
		{{
{CodeGenetaionHelper.Indent(3, GenerateConstructor(type))}
		}}
	}}
}}
";
		}

		private static string GenerateConstructor(JsonType type)
		{
			return @$"
HasValue = true;

{GenerateFieldInitializers(type)}

if (jsonReader.TokenType == JsonTokenType.None) jsonReader.Read();
if (jsonReader.TokenType != JsonTokenType.StartObject) throw new JsonException(""Expected '{{'"");

while (jsonReader.TokenType != JsonTokenType.EndObject)
{{
	if (!jsonReader.Read()) throw new JsonException(""Expected '}}'"");
	if (jsonReader.TokenType != JsonTokenType.PropertyName) throw new JsonException(""Expected property name"");

{CodeGenetaionHelper.Indent(1, GenerateFieldDeserializers(type))}
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

		private static string GenerateFieldDeserializers(JsonType type)
		{
			return CodeGenetaionHelper.JoinLines("else ", type.Fields.Select(GenerateFieldDeserializer));
		}

		private static string GenerateFieldDeserializer(JsonField field)
		{
			return $@"
if ({GenerateFieldNameCondition(field)})
{{
	jsonReader.Read();
	{field.Name} = {GenerateFieldDeserializer(field.Type)};
}}";
		}

		private static string GenerateFieldNameCondition(JsonField field)
		{
			return string.Join(" || ", field.SerializedNames.Select(name => @$"jsonReader.ValueTextEquals(""{name}"")"));
		}

		private static string GenerateFieldDeserializer(string type)
		{
			switch (type)
			{
				case "System.Int32":
				case "System.Double":
					return $"jsonReader.{type.Split('.')[1]}()";
				default:
					return $"new {type}(jsonReader)";
			}
		}
	}
}
