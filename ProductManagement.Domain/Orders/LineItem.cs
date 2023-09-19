using System.Threading.Channels;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Products;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Domain.Orders;

public class LineItem : BaseEntity
{
    private LineItem(Guid productId, Money price)
    {
        ProductId = productId;
        Price = price;
    }

    public Guid LineItemId { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public Money Price { get; private set; }

    public static LineItem CreateLineItem(Product product)
    {
        return new LineItem(product.Id, product.Price);
    }
}