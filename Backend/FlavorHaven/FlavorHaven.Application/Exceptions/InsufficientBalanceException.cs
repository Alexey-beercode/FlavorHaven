namespace FlavorHaven.Application.Exceptions;

public class InsufficientBalanceException : Exception
{
    public Guid UserId { get; }
    public decimal CurrentBalance { get; }
    public decimal RequiredAmount { get; }

    public InsufficientBalanceException(Guid userId, decimal currentBalance, decimal requiredAmount)
        : base($"Insufficient balance. Current balance: {currentBalance}, Required amount: {requiredAmount}")
    {
        UserId = userId;
        CurrentBalance = currentBalance;
        RequiredAmount = requiredAmount;
    }
}