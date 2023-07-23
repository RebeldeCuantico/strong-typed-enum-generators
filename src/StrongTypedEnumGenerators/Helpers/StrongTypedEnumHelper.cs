namespace RCTools.SourceGenerator.StrongTypedEnumGenerators.Helpers;

internal class PropertiesHelper
{
    private PropertiesHelper() { }

    public PropertiesHelper(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    public string Name { get; private set; }
}

