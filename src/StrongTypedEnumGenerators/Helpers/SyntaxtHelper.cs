using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace RCTools.SourceGenerator.StrongTypedEnumGenerators.Helpers;

internal static class SyntaxtHelper
{
    internal static NamespaceDeclarationSyntax FindNamespace(ClassDeclarationSyntax classDeclaration)
    {
        if (classDeclaration.Parent is NamespaceDeclarationSyntax namespaceDeclaration)
        {
            return namespaceDeclaration;
        }

        if (classDeclaration.Parent is ClassDeclarationSyntax parentClassDeclaration)
        {
            return FindNamespace(parentClassDeclaration);
        }

        return null;
    }

    internal static string GetParentNames(ClassDeclarationSyntax classDeclaration)
    {
        var sb = new StringBuilder();
        AppendParentNamesRecursive(classDeclaration.Parent, sb);
        return sb.ToString().TrimEnd('.');
    }

    private static void AppendParentNamesRecursive(SyntaxNode parent, StringBuilder sb)
    {
        if (parent == null)
        {
            return;
        }

        if (parent is ClassDeclarationSyntax classDeclaration )
        {
            sb.Insert(0, $".{classDeclaration.Identifier.ValueText}");
            AppendParentNamesRecursive(classDeclaration.Parent, sb);
        }

        if (parent is NamespaceDeclarationSyntax namespaceDeclaration)
        {
            sb.Insert(0, $"{namespaceDeclaration.Name}");
        }
    }
}

