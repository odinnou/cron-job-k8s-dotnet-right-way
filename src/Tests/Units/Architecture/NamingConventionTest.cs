using ArchUnitNET.xUnit;
using Tests.Configuration;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture;

public class NamingConventionTest : BaseArchitectureTest
{
    [Fact]
    public void Interfaces_that_resides_in_Core_Ports_namespace_should_have_name_ending_with_Port()
    {
        // arrange act assert
        Interfaces().That().ResideInNamespace("Runner.Core.Ports").Should().HaveNameEndingWith("Port").Check(Architecture);
    }
}
