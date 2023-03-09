using ArchUnitNET.xUnit;
using Tests.Configuration;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture;

public class JobConventionTest : BaseArchitectureTest
{
    [Fact]
    public void Classes_that_reside_in_Jobs_namespace_should_be_assignable_to_JobProcessorBase()
    {
        // arrange act assert
        Classes().That().ResideInNamespace("Runner.Core.UseCases.Jobs").Should().BeAssignableTo("Runner.Core.UseCases.Jobs.JobProcessorBase").Check(Architecture);
    }
}
