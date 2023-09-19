using ProductManagement.Domain.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.Domain.Orders;

public class Order : BaseEntity
{
    private Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
    }
    public Guid CustomerId { get; private set; }

    private List<LineItem> _lineItems = new();

    public static Order Create(Guid customerId)
    {
        return new Order(customerId);
    }

    public void Add(Product product)
    {
        _lineItems.Add(LineItem.CreateLineItem(product));
    }
}