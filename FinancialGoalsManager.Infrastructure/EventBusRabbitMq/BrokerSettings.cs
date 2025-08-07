namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public sealed record BrokerSettings
{
    public string Host { get; init; } = null!;
    public string Uri { get; init; } = null!;
    public int Port { get; init; }
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string BrokerName { get; init; } = null!;
    public string QueueName { get; init; } = null!;
}