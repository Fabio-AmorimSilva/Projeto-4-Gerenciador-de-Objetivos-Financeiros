namespace FinancialGoalsManager.Infrastructure.Auth.Services;

public sealed class PasswordHashService : IPasswordHashService
{
    public string HashPassword(string password)
    {
        string combinedPassword = password;

        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);

            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("x2"));

            return result.ToString();
        }
    }
}