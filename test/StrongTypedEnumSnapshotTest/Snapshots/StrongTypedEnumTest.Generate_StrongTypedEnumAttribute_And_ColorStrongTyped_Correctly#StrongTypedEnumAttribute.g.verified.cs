//HintName: StrongTypedEnumAttribute.g.cs
    namespace RCTools.SourceGenerators.StrongTypedEnumGenerators
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