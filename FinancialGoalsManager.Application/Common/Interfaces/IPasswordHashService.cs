namespace FinancialGoalsManager.Application.Common.Interfaces;

public interface IPasswordHashService
{
    string HashPassword(string password);
}