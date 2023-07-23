//HintName: Color.g.cs
namespace RCTools.SourceGenerators.StrongTypedEnumGenerators
{
    public partial class Color
    {
       public static readonly Color Red = new(1, "Red");
public static readonly Color Blue = new(2, "Blue");
    

        public int Id { get; }

        public string Name { get; }

        public Color(int id, string name)
        {
            Id = id;
            Name = name;
        }
        
        public static readonly IReadOnlyList<Color> Fields = new List<Color> 
        {
           Red, Blue
        };

    }
}