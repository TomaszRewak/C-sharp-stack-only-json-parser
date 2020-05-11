using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

namespace StackOnlyJsonParser.CodeAnalysis
{
	internal static class SyntaxHelper
	{
		public static bool IsAutoProperty(IPropertySymbol property)
		{
			return property.DeclaringSyntaxReferences.Select(r => r.GetSyntax()).OfType<PropertyDeclarationSyntax>().Any(p => p.AccessorList?.Accessors.Any(ancestor => ancestor.IsKind(SyntaxKind.GetAccessorDeclaration) && ancestor.Body == null) == true);
		}
	}
}
