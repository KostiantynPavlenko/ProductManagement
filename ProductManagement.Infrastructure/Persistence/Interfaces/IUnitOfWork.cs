namespace ProductManagement.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}