using Microsoft.Extensions.Logging;
using Runner.Core.Models;
using System.Diagnostics;

namespace Runner.Core.UseCases.Jobs;

public abstract class JobProcessorBase : IJobProcessor
{
    protected readonly ILogger _logger;

    protected JobProcessorBase(ILogger logger)
    {
        _logger = logger;
    }

    public abstract Task Process();

    public abstract Job JobToProcess { get; }

    public async Task Execute()
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        _logger.LogInformation("{JobToProcess} started", JobToProcess.ToString());

        await Process();

        stopwatch.Stop();
        _logger.LogInformation("{JobToProcess} ended, elapsed time: {ElapsedMilliseconds} ms", JobToProcess, stopwatch.ElapsedMilliseconds);
    }
}
