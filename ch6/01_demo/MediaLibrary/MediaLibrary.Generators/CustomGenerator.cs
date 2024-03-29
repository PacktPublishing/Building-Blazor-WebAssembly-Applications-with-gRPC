﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;

namespace MediaLibrary.Generators;
[Generator]
public class CustomGenerator : ISourceGenerator
{
    const string AUTO_GENERATED_ATTRIBUTE = @"// <auto-generated>
//     Generated by SystemServiceGenerator.  DO NOT EDIT!
// </auto-generated>";

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver receiver)
        {
            throw new ArgumentException("Received invalid receiver in Execute step.");
        }

        foreach (ClassData item in receiver.Nodes)
        {
            string name = item.Node.Identifier.ToString();
            bool generateConstructor = GenerateConstructor(context.Compilation, item);

            var constructor = $@"    public {name}Service(MediaLibraryDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {{
        }}";

            var template = $@"{AUTO_GENERATED_ATTRIBUTE}
using AutoMapper;
using MediaLibrary.Server.Data;

namespace MediaLibrary.Server.Services;

public partial class {name}Service : BaseService<{name}, Shared.Model.{name}Model>
{{
    {(generateConstructor ? constructor : string.Empty)}
}}";
            context.AddSource($"{name}Service.g.cs", SourceText.From(template, Encoding.UTF8));
        }
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    private bool GenerateConstructor(Compilation compilation, ClassData item)
    {
        var semanticModel = compilation.GetSemanticModel(item.Node.SyntaxTree);
        var args = item.Attribute.ArgumentList?.Arguments[0];
        var expr = args?.Expression;
        var constant = semanticModel.GetConstantValue(expr);

        if (constant.HasValue && bool.TryParse(constant.ToString(), out var result))
        {
            return result;
        }

        return false;
    }

}
