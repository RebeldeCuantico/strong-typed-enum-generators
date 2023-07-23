using System.Runtime.CompilerServices;

namespace RCTools.Test.SourceGenerators.StrongTypedEnumSnapshotTest
{
    public static class ModuleInitializer
    {
        [ModuleInitializer]
        public static void Init()
        {
            VerifySourceGenerators.Initialize();
        }
    }
}
