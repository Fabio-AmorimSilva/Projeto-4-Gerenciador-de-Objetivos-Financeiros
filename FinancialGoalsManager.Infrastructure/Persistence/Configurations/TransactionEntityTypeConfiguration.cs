namespace FinancialGoalsManager.Infrastructure.Persistence.Configurations;

public sealed class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .ToTable("Transactions");
        
        builder
            .HasKey(t => t.Id);

        builder
            .Property(t => t.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .HasIndex(t => t.Id);
        
        builder
            .Property(t => t.Quantity)
            .IsRequired();
        
        builder
            .Property(t => t.Date)
            .IsRequired();
        
        builder
            .Property(t => t.Type)
            .IsRequired();
    }
}