namespace FinancialGoalsManager.Domain.EventBus;

public record IntegrationEvent
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
}