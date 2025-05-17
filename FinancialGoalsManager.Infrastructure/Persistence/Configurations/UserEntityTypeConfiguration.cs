namespace FinancialGoalsManager.Infrastructure.Persistence.Configurations;

public sealed class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("Users");
        
        builder
            .HasKey(u => u.Id);

        builder
            .HasIndex(u => u.Id);
        
        builder
            .Property(u => u.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(u => u.Name)
            .HasMaxLength(User.MaxNameLength)
            .IsRequired();
        
        builder
            .Property(u => u.Email)
            .IsRequired();

        builder
            .Property(u => u.Password)
            .IsRequired();
    }
}