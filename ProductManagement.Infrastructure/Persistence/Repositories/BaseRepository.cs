using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Common;
using ProductManagement.Domain.Common;
using ProductManagement.Infrastructure.Persistence.Interfaces;

namespace ProductManagement.Infrastructure.Persistence.Repositories;

public class BaseRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public BaseRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<List<T>> GetAll()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> Update(T entity)
    {
        var foundEntity = await _context.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == entity.Id);
        
        if (foundEntity == null)
        {
            return false;
        }
        _context.Set<T>().Update(entity);
        
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> Delete(Guid id)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
        {
            return false;
        }
        _context.Set<T>().Remove(entity);
        return await _unitOfWork.SaveChangesAsync();
    }
}