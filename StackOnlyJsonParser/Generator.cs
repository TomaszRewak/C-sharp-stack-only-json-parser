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
		public void Initialize(GeneratorInitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
		}

		public void Execute(GeneratorExecutionContext context)
		{
			var syntaxReceiver = (MySyntaxReceiver)context.SyntaxReceiver;
			var compilation = context.Compilation;

			foreach (var classSyntax in syntaxReceiver.Structs)
			{
				var semanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
				var type = semanticModel.GetDeclaredSymbol(classSyntax);

				if (type.HasAttribute(typeof(StackOnlyJsonTypeAttribute).FullName))
					GenerateType(context, type);

				if (type.HasAttribute(typeof(StackOnlyJsonArrayAttribute).FullName))
					GenerateArray(context, type);

				if (type.HasAttribute(typeof(StackOnlyJsonDictionaryAttribute).FullName))
					GenerateDictionaty(context, type);

				if (type.HasAttribute(typeof(StackOnlyJsonLazyLoaderAttribute).FullName))
					GenerateLazyLoader(context, type);
			}
		}

		private void GenerateType(GeneratorExecutionContext context, INamedTypeSymbol type)
		{
			var structure = new JsonType(type);
			var code = TypeGenerator.Generate(structure);

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
		}

		private void GenerateArray(GeneratorExecutionContext context, INamedTypeSymbol type)
		{
			var structure = new JsonArray(type);
			var code = ArrayGenerator.Generate(structure);

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
		}

		private void GenerateDictionaty(GeneratorExecutionContext context, INamedTypeSymbol type)
		{
			var structure = new JsonDictionary(type);
			var code = DictionaryGenerator.Generate(structure);

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
		}

		private void GenerateLazyLoader(GeneratorExecutionContext context, INamedTypeSymbol type)
		{
			var structure = new JsonLazyLoader(type);
			var code = LazyLoaderGenerator.Generate(structure);

			context.AddSource($"{type.Name}.Generated.cs", SourceText.From(code, Encoding.UTF8));
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
