using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StackOnlyJsonParser
{
    [Generator]
    public class StackOnlyJsonParserGenerator : ISourceGenerator
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

            foreach (var classSyntax in syntaxReceiver.Classes)
            {
                var semanticModel = compilation.GetSemanticModel(classSyntax.SyntaxTree);
                var typeSymbol = semanticModel.GetDeclaredSymbol(classSyntax);
                var attribute = typeSymbol.GetAttributes().FirstOrDefault(a => SymbolEqualityComparer.Default.Equals(a.AttributeClass, attributeSymbol));

                if (attribute == null) continue;

                var typeName = attribute.ConstructorArguments[0].Value as string ?? $"StackOnly{typeSymbol.Name}Parser";
                var typeNamespace = attribute.ConstructorArguments[1].Value as string ?? GetNamespace(typeSymbol);

                //Console.Error.WriteLine(typeNamespace);

                context.AddSource("StackOnlyJsonParser.Generated.cs", SourceText.From($@"
namespace {typeNamespace}
{{
            public class {typeName}
    {{
        public static void GeneratedMethod()
        {{
            // generated code
        }}
        public static void GeneratedMethod2()
        {{
            // generated code
        }}
        internal static void GeneratedMethod3()
        {{
            // generated code
        }}
    }}
}}", Encoding.UTF8));
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
        public List<ClassDeclarationSyntax> Classes = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax && classDeclarationSyntax.AttributeLists.Count > 0)
            {
                Classes.Add(classDeclarationSyntax);
            }
        }
    }
}
