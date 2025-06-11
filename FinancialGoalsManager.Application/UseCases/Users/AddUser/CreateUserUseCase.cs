namespace FinancialGoalsManager.Application.UseCases.Users.AddUser;

public sealed class CreateUserUseCase(
    IFinancialGoalManagerDbContext context,
    IPasswordHashService passwordHashService
) : ICreateUserUseCase
{
    public async Task<UseCaseResult<Guid>> ExecuteAsync(CreateUserInputModel model)
    {
        var hashedPassword = passwordHashService.HashPassword(model.Password);

        var user = new User(
            name: model.Name,
            email: model.Email,
            password: hashedPassword
        );

        var emailAlreadyExists = await context.Users
            .WithSpecification(new EmailAlreadyExistsSpec(user.Id, user.Email))
            .AnyAsync();

        if (emailAlreadyExists)
            throw new NotFoundException(ErrorMessages.EmailAlreadyExists(user.Email));

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        return UseCaseResult<Guid>.Success(user.Id);
    }
}