namespace ProductManagement.Domain.ValueObjects;

public class Money
{
    public Money()
    {
        
    }
    
    private Money(decimal amount) => Amount = amount;
    public decimal Amount { get; }

    public static Money Create(decimal amount)
    {
        return new Money(amount);
    }
};