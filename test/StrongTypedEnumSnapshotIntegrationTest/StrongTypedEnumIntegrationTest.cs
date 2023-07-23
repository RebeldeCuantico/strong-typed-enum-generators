using RCTools.SourceGenerators.StrongTypedEnumGenerators;

namespace RCTools.Test.SourceGenerators.StrongTypedEnumSnapshotIntegrationTest
{
    [StrongTypedEnum("Red")]
    public partial class Color { }


    [StrongTypedEnum("FieldName", "FieldDescription")]
    public partial class TypicalFields { }

    public class StrongTypedEnumTest
    {
        [Fact]
        public void Color_Red_Must_Be_Ok()
        {
            Color.Red.Name.Should().Be("Red");
            Color.Red.Id.Should().Be(1);
            Color.Fields.Count.Should().Be(1);
            Color.Fields[0].Should().Be(Color.Red);
        }

        [Fact]
        public void Typical_Fields_Must_Be_Created()
        {
            TypicalFields.FieldName.Name.Should().Be("FieldName");
            TypicalFields.FieldName.Id.Should().Be(1);

            TypicalFields.FieldDescription.Name.Should().Be("FieldDescription");
            TypicalFields.FieldDescription.Id.Should().Be(2);

            TypicalFields.Fields.Count.Should().Be(2);
        }
    }
}