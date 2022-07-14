using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediaLibrary.Generators;

internal class ClassData
{
    public ClassData(ClassDeclarationSyntax node, AttributeSyntax attribute)
    {
        Node = node;
        Attribute = attribute;
    }

    public ClassDeclarationSyntax Node { get; set; }
    public AttributeSyntax Attribute { get; set; }
}
