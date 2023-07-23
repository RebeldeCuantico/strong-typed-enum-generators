using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using RCTools.SourceGenerator.StrongTypedEnumGenerators;

namespace RCTools.Test.SourceGenerators.StrongTypedEnumSnapshotTest
{
    [UsesVerify]
    public class StrongTypedEnumTest
    {
        [Fact]
        public Task Generate_StrongTypedEnumAttribute_And_ColorStrongTyped_Correctly()
        {
            var code = """
                using RCTools.SourceGenerators.StrongTypedEnumGenerators;              

                [StrongTypedEnum("Red", "Blue")]
                public partial class Color { }
                """;

            return Verify(code);
        }

        [Fact]
        public Task Generate_StrongTypedEnumAttribute_And_ColorStrongTyped_Inside_A_Class_Correctly()
        {
            var code = """
                using RCTools.SourceGenerators.StrongTypedEnumGenerators;              
                public class UpperClass{
                [StrongTypedEnum("Red", "Blue")]
                public partial class Color { }
                }
                """;
        

            return Verify(code);
        }

        private static Task Verify(string code)
        {
            var syntaxTree = CSharpSyntaxTree.ParseText(code);

            IEnumerable<PortableExecutableReference> references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            };

            var compilation = CSharpCompilation.Create(
                assemblyName: "Tests",
                syntaxTrees: new[] { syntaxTree },
                references: references);

            var generator = new StrongTypedEnumGenerator();

            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

            driver = driver.RunGenerators(compilation);

            return Verifier.Verify(driver).UseDirectory("Snapshots");
        }
    }
}
