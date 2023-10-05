using System.Reflection;
using Moq;
using ProductManagement.Application.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.API.IntegrationTests.Moqs;

public class MockFactory
{
    public MockFactory()
    {
        ProductRepository = new Mock<IRepository<Product>>();
    }
    
    public Mock<IRepository<Product>> ProductRepository { get; }

    public IEnumerable<(Type, object)> GetMocks()
    {
        return GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(x =>
            {
                var interfaceType = x.PropertyType.GetGenericArguments()[0];
                var value = x.GetValue(this) as Mock;

                return (interfaceType, value.Object);
            }).ToArray();
    }
}