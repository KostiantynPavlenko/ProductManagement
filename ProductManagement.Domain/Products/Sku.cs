namespace ProductManagement.Domain.Products;

public class Sku
{
    public Sku()
    {
        
    }
    private const int DefaultLength = 15;
    public string Value { get; }
    private Sku(string value) => Value = value;

    public static Sku? Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < DefaultLength)
        {
            return default;
        }

        return new Sku(value);
    }
}