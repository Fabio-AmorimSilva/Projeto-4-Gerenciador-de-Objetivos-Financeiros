﻿namespace FinancialGoalsManager.Infrastructure.EventBusRabbitMq;

public class EventBusOptions
{
    public string SubscriptionClientName { get; set; }
    public int RetryCount { get; set; } = 10;
}