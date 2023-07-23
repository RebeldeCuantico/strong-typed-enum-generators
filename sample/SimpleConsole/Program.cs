using RCTools.SourceGenerators.StrongTypedEnumGenerators;

namespace RCTools.Samples.SourceGenerators.SimpleConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Color.Red.Name);

            Console.WriteLine(CategoryFieldName.CategoryName.Name);

            DaysOfWeekStrong day = DaysOfWeekStrong.Monday;

            Console.WriteLine(DaysOfWeekStrong.Sunday == day);
        }
    }

    [StrongTypedEnum("Red", "Blue")]
    public partial class Color { }

    [StrongTypedEnum("CategoryName", "CategoryDescription")]
    public partial class CategoryFieldName { }

    [StrongTypedEnum("Monday","Tuesday","Wednesday","Thrusday", "Friday", "Saturday", "Sunday")]
    public partial class  DaysOfWeekStrong
    {        
    }
}