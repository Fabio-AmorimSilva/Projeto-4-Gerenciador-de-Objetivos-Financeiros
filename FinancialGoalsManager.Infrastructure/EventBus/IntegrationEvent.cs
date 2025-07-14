namespace FinancialGoalsManager.Infrastructure.EventBus;

public record IntegrationEvent
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }

    public IntegrationEvent(
        Guid id,
        DateTime createdAt
    )
    {
        Id = id;
        CreatedAt = createdAt;
    }
}