namespace FinancialGoalsManager.Infrastructure.Persistence.Configurations;

public sealed class FinancialGoalEntityTypeConfiguration : IEntityTypeConfiguration<FinancialGoal>
{
    public void Configure(EntityTypeBuilder<FinancialGoal> builder)
    {
        builder
            .ToTable("FinancialGoals");
        
        builder
            .HasKey(fg => fg.Id);
        
        builder
            .Property(fg => fg.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .HasIndex(fg => fg.Id);

        builder
            .Property(fg => fg.Title)
            .HasMaxLength(FinancialGoal.TitleMaxLength)
            .IsRequired();

        builder
            .Property(fg => fg.Goal)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder
            .Property(fg => fg.DueDate)
            .IsRequired();

        builder
            .Property(fg => fg.MonthGoal)
            .IsRequired(false);

        builder
            .Property(fg => fg.Status)
            .IsRequired();
        
        builder
            .HasOne(fg => fg.User)
            .WithMany(u => u.FinancialGoals)
            .HasForeignKey(fg => fg.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}