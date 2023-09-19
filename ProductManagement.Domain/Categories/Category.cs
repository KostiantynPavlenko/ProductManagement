using System.Security.Cryptography.X509Certificates;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Products;

namespace ProductManagement.Domain.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public Product Product { get; set; }
}