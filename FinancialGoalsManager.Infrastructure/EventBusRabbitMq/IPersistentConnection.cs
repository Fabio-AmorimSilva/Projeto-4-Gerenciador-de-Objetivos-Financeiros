﻿namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public interface IPersistentConnection
{
    event EventHandler OnReconnectedAfterConnectionFailure;
    bool IsConnected { get; }

    bool TryConnect();
    IModel CreateModel();
}