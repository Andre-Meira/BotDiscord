using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TwitchService.Services.Auth;

namespace BotDiscord.Services.HandlerHosted;

public abstract class TwitchHandler : IHostedService, IDisposable
{    
    public static TokenObjectResponse Token { get; set; }    
    private readonly ILogger<TwitchHandler> _logger;
    private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
    private Task _executingTask;
    public TwitchHandler(ILogger<TwitchHandler> logger) 
    {
        _logger = logger;
    }

    protected abstract Task ExecuteAsync(CancellationToken cancellationToken);

    public virtual Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("---Start Service");   
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
            _logger.LogInformation("--StopService");
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,cancellationToken));
        }
    }

    public void Dispose()
    {
        _executingTask.Dispose();
    }
}
