using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using StackOnlyJsonParser.CodeAnalysis;
using StackOnlyJsonParser.CodeGeneration;
using StackOnlyJsonParser.CodeStructure;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackOnlyJsonParser
{
	[Generator]
	internal class Generator : ISourceGenerator
	{
		public void Initialize(InitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
		}

		public void Execute(SourceGeneratorContext context)
		{
			var syntaxReceiver = (MySyntaxReceiver)context.SyntaxReceiver;
			var compilation = context.Compilation;

			var typeAttributeSymbol = compilation.GetTypeByMetadataName(typeof(StackOnlyJsonTypeAttribute).FullName);
			var arrayAttributeSymbol = compilation.GetTypeByMetadataName(typeof(StackOnlyJsonArrayAttribute).FullName);
			var dictionaryAttributeSymbol = compilation.GetTypeByMetadataName(typeof(StackOnlyJsonDictionaryAttribute).FullName);

			foreach (var classSyntax in syntaxReceiver.Structs)
			{
				var semanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
				var type = semanticModel.GetDeclaredSymbol(classSyntax);

				if (type.HasAttribute(typeAttributeSymbol))
					GenerateType(context, type);

				if (type.HasAttribute(arrayAttributeSymbol))
					GenerateArray(context, type, type.GetAttribute(arrayAttributeSymbol));

				if (type.HasAttribute(dictionaryAttributeSymbol))
					GenerateDictionaty(context, type, type.GetAttribute(dictionaryAttributeSymbol));
			}
		}

		private void GenerateType(SourceGeneratorContext context, INamedTypeSymbol type)
		{
			var jsonFields = type
				.GetMembers()
				.OfType<IPropertySymbol>()
				.Where(SyntaxHelper.IsAutoProperty)
				.Select(field => new JsonField(
					field.Name,
					field.Type.GetFullName(),
					new[] { field.Name }));

			var structure = new JsonType(
				"public",
				type.GetNamespace(),
				type.Name,
				jsonFields);

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(TypeGenerator.Generate(structure), Encoding.UTF8));
		}

		private void GenerateArray(SourceGeneratorContext context, INamedTypeSymbol type, AttributeData attributeData)
		{
			var structure = new JsonArray(
				"public",
				type.GetNamespace(),
				type.Name,
				((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value).GetFullName());

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(ArrayGenerator.Generate(structure), Encoding.UTF8));
		}

		private void GenerateDictionaty(SourceGeneratorContext context, INamedTypeSymbol type, AttributeData attributeData)
		{
			var structure = new JsonDictionary(
				"public",
				type.GetNamespace(),
				type.Name,
				((INamedTypeSymbol)attributeData.ConstructorArguments[0].Value).GetFullName(),
				((INamedTypeSymbol)attributeData.ConstructorArguments[1].Value).GetFullName());

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(DictionaryGenerator.Generate(structure), Encoding.UTF8));
		}
	}

	internal class MySyntaxReceiver : ISyntaxReceiver
	{
		public List<StructDeclarationSyntax> Structs = new List<StructDeclarationSyntax>();

		public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
		{
			if (syntaxNode is StructDeclarationSyntax structDeclarationSyntax && structDeclarationSyntax.AttributeLists.Count > 0)
			{
				Structs.Add(structDeclarationSyntax);
			}
		}
	}
}
