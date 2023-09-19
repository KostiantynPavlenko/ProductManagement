using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductManagement.Infrastructure.Persistence;

public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        optionBuilder.UseSqlServer(
            @"Password=Pilgrim!;User ID=smartadmin;Data Source=.\sql2016;Initial Catalog=Products;TrustServerCertificate=True;");

        return new ApplicationDbContext(optionBuilder.Options);
    }
}