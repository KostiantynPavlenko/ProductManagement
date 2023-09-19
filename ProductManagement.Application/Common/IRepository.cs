namespace ProductManagement.Application.Common;

public interface IRepository<T> where T: class
{
    Task<List<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<bool> Create(T product);
    Task<bool> Update(T product);
    Task<bool> Delete(Guid id);
}