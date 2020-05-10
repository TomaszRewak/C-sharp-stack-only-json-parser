using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using StackOnlyJsonParser.CodeGeneration;
using StackOnlyJsonParser.CodeStructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StackOnlyJsonParser
{
	[Generator]
	public class StackOnlyJsonCodeGenerator : ISourceGenerator
	{
		public void Initialize(InitializationContext context)
		{
			context.RegisterForSyntaxNotifications(() => new MySyntaxReceiver());
		}

		public void Execute(SourceGeneratorContext context)
		{
			var syntaxReceiver = (MySyntaxReceiver)context.SyntaxReceiver;
			var compilation = context.Compilation;

			var attributeSymbol = compilation.GetTypeByMetadataName(typeof(StackOnlyJsonTypeAttribute).FullName);

			//Console.Error.WriteLine("Aaaa");

			foreach (var classSyntax in syntaxReceiver.Structs)
			{
				var semanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
				var typeSymbol = semanticModel.GetDeclaredSymbol(classSyntax);
				var attribute = typeSymbol.GetAttributes().FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeSymbol));

				if (attribute == null) continue;

				var typeName = typeSymbol.Name;
				var typeNamespace = GetNamespace(typeSymbol);
				var fields = typeSymbol.GetMembers().OfType<IFieldSymbol>();

				var jsonFields = fields
					.Select(field => new JsonField(
						field.Name,
						$"{GetNamespace(field.Type)}.{field.Type.Name}",
						new[] { field.Name }));

				var structure = new JsonType(
					Accessibility.Public,
					typeNamespace,
					typeName,
					jsonFields);

				context.AddSource($"{typeName}.Generated.cs", SourceText.From(TypeGenerator.Generate(structure), Encoding.UTF8));
			}
		}

		private string GetNamespace(ISymbol symbol)
		{
			var namespaces = new List<INamespaceSymbol>();

			for (var currentNamespace = symbol.ContainingNamespace; !currentNamespace.IsGlobalNamespace; currentNamespace = currentNamespace.ContainingNamespace)
				namespaces.Add(currentNamespace);

			return string.Join(".", namespaces.Select(n => n.Name).Reverse());
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
