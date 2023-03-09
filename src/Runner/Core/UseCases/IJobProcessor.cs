using Runner.Core.Models;

namespace Runner.Core.UseCases;

public interface IJobProcessor
{
    Job JobToProcess { get; }
    Task Execute();
}
