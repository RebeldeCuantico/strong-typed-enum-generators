using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace RCTools.SourceGenerator.StrongTypedEnumGenerators;

using Helpers;

[Generator]
public class StrongTypedEnumGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
       //if (!Debugger.IsAttached) Debugger.Launch(); //For testing purposes, uncomment this line and add the "using System.Diagnostics" statement.

        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
        StringHelper.StrongTypedEnumAttributeFileName,
        SourceText.From(StringHelper.StrongTypedEnumAttributeTemplate, Encoding.UTF8)));

        var provider = context.SyntaxProvider.CreateSyntaxProvider(
           predicate: (c, _) => c is ClassDeclarationSyntax m && m.AttributeLists.Count > 0,
           transform: (n, _) => GetSemanticTargetForGeneration(n)
           ).Where(m => m is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation,
           (spc, source) => Execute(spc, source.Left, source.Right));
    }

    private static void Execute(SourceProductionContext context, Compilation compilation, ImmutableArray<ClassDeclarationSyntax> typeList)
    {
        var distinctEnums = typeList.Distinct();

        foreach (var strongTypedEnum in distinctEnums)
        {
            var className = strongTypedEnum.Identifier.ValueText;
            AttributeSyntax attributeSyntax = null;
            var @namespace = StringHelper.StrongTypedEnumAttributeNamespace;

            foreach (var attributes in strongTypedEnum.AttributeLists)
            {
                foreach (var attribute in attributes.Attributes)
                {
                    if (((IdentifierNameSyntax)attribute.Name).Identifier.ValueText == StringHelper.StrongTypedEnumName)
                    {
                        attributeSyntax = attribute;
                        var namespaceNode = SyntaxtHelper.FindNamespace(strongTypedEnum);

                        if (namespaceNode != null)
                        {
                            @namespace = namespaceNode.Name.ToString();
                        }
                    }
                }
            }

            if (attributeSyntax is not null)
            {
                var enums = new List<PropertiesHelper>();
                var count = 1;
                foreach (var argument in attributeSyntax.ArgumentList.Arguments)
                {
                    var @enum = new PropertiesHelper(count++,
                                                     ((LiteralExpressionSyntax)argument.Expression).Token.ValueText);

                    enums.Add(@enum);
                }

                var code = StringHelper.GetStrongTypedEnumTemplate(@namespace, className, enums);

                context.AddSource($"{className}.g.cs", code);
            }
        }
    }

    private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        foreach (AttributeListSyntax attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var fullName = attributeContainingTypeSymbol.ToDisplayString();

                if (fullName == StringHelper.StrongTypedEnumAttributeFullName)
                {
                    return classDeclarationSyntax;
                }
            }
        }

        return null;
    }
}

