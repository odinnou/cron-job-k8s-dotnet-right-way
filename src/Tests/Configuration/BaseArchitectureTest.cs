using ArchUnitNET.Loader;
using Runner;

namespace Tests.Configuration;

public abstract class BaseArchitectureTest
{
    protected static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(typeof(Program).Assembly).Build();
}
