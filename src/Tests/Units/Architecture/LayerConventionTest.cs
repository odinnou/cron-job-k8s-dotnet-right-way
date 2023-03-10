using ArchUnitNET.Domain;
using ArchUnitNET.xUnit;
using Tests.Configuration;
using Xunit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Tests.Units.Architecture;

public class LayerConventionTest : BaseArchitectureTest
{
    private readonly IObjectProvider<IType> _coreLayer = Types().That().ResideInNamespace("Runner.Core.*", useRegularExpressions: true).As("Core layer");
    private readonly IObjectProvider<IType> _drivenAdaptersLayer = Types().That().ResideInNamespace("Runner.DrivenAdapters.*", useRegularExpressions: true).As("Driven adapters layer");

    [Fact]
    public void Types_that_resides_in_Core_layer_should_not_depend_on_any_types_that_reside_in_Driven_adapters_layer_and_not_depend_on_any_types_that_reside_in_Driving_adapters_layer()
    {
        // arrange act assert
        Types().That().Are(_coreLayer).Should().NotDependOnAny(_drivenAdaptersLayer).Because("it's forbidden").Check(Architecture);
    }
}
