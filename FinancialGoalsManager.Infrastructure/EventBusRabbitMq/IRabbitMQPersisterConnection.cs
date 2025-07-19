namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public interface IRabbitMqPersisterConnection : IDisposable
{
    bool IsConnected { get; }

    Task<bool> TryConnect();

    Task<IChannel> CreateModel();
}