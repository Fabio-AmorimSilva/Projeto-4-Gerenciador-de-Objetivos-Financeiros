namespace FinancialGoalsManager.Domain.Messages;

public static class ErrorMessages
{
    public static string CannotBeEmpty(string field)
        => $"{field} cannot be empty.";

    public static string NotFound<T>()
        => $"{typeof(T).Name} not Found.";

    public static string HasMaxLength(string field, int maxLength)
        => $"{field} cannot be more than {maxLength} characters.";

    public static string IsInvalidEnum(string field)
        => $"{field} is not valid.";

    public static string HasToBeGreaterThan(string field, int value)
        => $"{field} must be greater than {value}.";

    public static string EmailAlreadyExists(string email)
        => $"{email} is already registered.";
    
    public static string EmailOrPasswordAreIncorrect()
        => "Email or password are incorrect.";
}