using ProductManagement.Domain.Categories;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Domain.Products;

public class Product : BaseEntity
{
    public Product()
    {
        
    }
    private Product(string name, Guid categoryId, Sku sku, Money price)
    {
        Id = Guid.NewGuid();
        Name = name;
        CategoryId = categoryId;
        Sku = sku;
        Price = price;
    }
    
    private Product(Guid id, string name, Guid categoryId, Sku sku, Money price)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
        Sku = sku;
        Price = price;
    }
    
    public string Name { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }

    public Sku Sku { get; private set; }

    public Money Price { get; private set; }

    public static Product Create(string name, Guid categoryId, string sku, decimal price)
    {
        return new Product(name, categoryId, Sku.Create(sku), Money.Create(price));
    }
    
    public static Product Create(Guid id, string name, Guid categoryId, string sku, decimal price)
    {
        return new Product(id, name, categoryId, Sku.Create(sku), Money.Create(price));
    }
}