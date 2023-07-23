using System.Collections.Generic;
using System.Text;

namespace RCTools.SourceGenerator.StrongTypedEnumGenerators.Helpers;

internal static class StringHelper
{
    internal const string StrongTypedEnumAttributeFileName = "StrongTypedEnumAttribute.g.cs";

    internal const string StrongTypedEnumName = "StrongTypedEnum";

    internal const string StrongTypedEnumAttributeFullName = "RCTools.SourceGenerators.StrongTypedEnumGenerators.StrongTypedEnumAttribute";

    internal const string StrongTypedEnumAttributeNamespace = "RCTools.SourceGenerators.StrongTypedEnumGenerators";

    internal const string StrongTypedEnumAttributeTemplate =
        $$"""
            namespace {{StrongTypedEnumAttributeNamespace}}
            {
                [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
                public sealed class StrongTypedEnumAttribute : System.Attribute
                {
                    public string[] Values { get; }
        
                    public StrongTypedEnumAttribute(params string[] values)
                    {
                        Values = values;
                    }
                }
            }
        """;

    internal static string GetStrongTypedEnumTemplate(string @namespace, string className, List<PropertiesHelper> enums)
    {
        return $$"""
                namespace {{@namespace}}
                {
                    public partial class {{className}}
                    {
                       {{GetStaticFields(enums, className)}}    

                        public int Id { get; }

                        public string Name { get; }

                        public {{className}}(int id, string name)
                        {
                            Id = id;
                            Name = name;
                        }
                        
                        public static readonly IReadOnlyList<{{className}}> Fields = new List<{{className}}> 
                        {
                           {{GetFields(enums)}}
                        };

                    }
                }
                """;
    }

    private static string GetFields(List<PropertiesHelper> fields)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < fields.Count; i++)
        {
            sb.Append(fields[i].Name);
            if (i < fields.Count - 1) sb.Append(", ");
        }

        return sb.ToString();
    }

    private static string GetStaticFields(List<PropertiesHelper> fields, string className)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < fields.Count; i++)
        {
            sb.AppendLine($"public static readonly {className} {fields[i].Name} = new({fields[i].Id}, \"{fields[i].Name}\");");
        }

        return sb.ToString();
    }

}

