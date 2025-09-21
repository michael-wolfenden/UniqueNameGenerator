using PublicApiGenerator;
using UniqueNameGenerator;

namespace UniqueNameGenerator.Tests;

public class PublicApiSpecs
{
    [Fact]
    public Task public_apis_have_not_changed() =>
        Verify(typeof(UniqueName).Assembly.GeneratePublicApi());
}
