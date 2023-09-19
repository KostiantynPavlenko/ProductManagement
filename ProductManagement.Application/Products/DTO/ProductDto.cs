namespace ProductManagement.Application.Products.DTO;

public record ProductDto
{
    public ProductDto()
    {
        
    }
    
    public Guid ProductId { get; init; }
    public string Name { get; init; }
    public Guid CategoryId { get;  init;}
    public decimal Price { get; init; }
    public string Sku { get; init;}
}