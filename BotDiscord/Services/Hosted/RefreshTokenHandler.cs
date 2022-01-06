using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BotDiscord.Services.Hosted;

public abstract class RefreshTokenHandler : IHostedService, IDisposable
{
    private readonly ILogger<RefreshTokenHandler> _logger;
    private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
    private Task _executingTask; 
    public RefreshTokenHandler(ILogger<RefreshTokenHandler> logger)
    {
        _logger = logger;     
    }

    protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

    public virtual Task StartAsync(CancellationToken cancellationToken)
    {
        
        _executingTask = ExecuteAsync(_stoppingCts.Token);

        if (_executingTask.IsCompleted)
        {
            return _executingTask;
        }         
        return Task.CompletedTask;        
    }

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_executingTask == null)        
            return;
        
        try
        {            
            _stoppingCts.Cancel();

        }finally
        {            
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,cancellationToken));
        }
    }

    public void Dispose()
    {
        _executingTask.Dispose();
    }
}
