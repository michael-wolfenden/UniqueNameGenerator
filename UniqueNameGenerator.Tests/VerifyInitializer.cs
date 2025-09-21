using System.Runtime.CompilerServices;

namespace UniqueNameGenerator.Tests;

public static class VerifyInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        UseSourceFileRelativeDirectory("Snapshots");
    }
}
