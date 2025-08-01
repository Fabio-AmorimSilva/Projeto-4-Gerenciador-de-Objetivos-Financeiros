﻿namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public interface IRabbitMqPersisterConnection : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}