using ArchUnitNET.xUnit;
using Tests.Configuration;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture;

public class NamingConventionTest : BaseArchitectureTest
{
    [Fact]
    public void Classes_that_assignable_to_Profile_should_have_name_ending_with_MappingProfile()
    {
        // arrange act assert
        Classes().That().AreAssignableTo("AutoMapper.Profile").Should().HaveNameEndingWith("MappingProfile").Check(Architecture);
    }

    [Fact]
    public void Classes_that_resides_in_DatabaseAdapters_Entities_namespace_should_have_name_ending_with_Entity()
    {
        // arrange act assert
        Classes().That().ResideInNamespace("Runner.DrivenAdapters.DatabaseAdapters.Entities").And().DoNotResideInNamespace("Runner.DrivenAdapters.DatabaseAdapters.Entities.Mappings").Should().HaveNameEndingWith("Entity").Check(Architecture);
    }

    [Fact]
    public void Interfaces_that_resides_in_Core_Ports_namespace_should_have_name_ending_with_Port()
    {
        // arrange act assert
        Interfaces().That().ResideInNamespace("Runner.Core.Ports").Should().HaveNameEndingWith("Port").Check(Architecture);
    }

    [Fact]
    public void Classes_that_assignable_to_System_Exception_should_have_name_ending_with_Exception()
    {
        // arrange act assert
        Classes().That().AreAssignableTo("System.Exception").Should().HaveNameEndingWith("Exception").Check(Architecture);
    }
}
