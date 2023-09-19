using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Common;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infrastructure.Identity.Interfaces;
using ProductManagement.Infrastructure.Persistence.Interfaces;

namespace ProductManagement.Infrastructure.Persistence.Core;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private readonly IDateTime _dateService;
    private readonly IUserAccessor _userAccessor;


    public UnitOfWork(ApplicationDbContext context, 
        IDateTime dateService,
        IUserAccessor userAccessor)
    {
        _context = context;
        _dateService = dateService;
        _userAccessor = userAccessor;
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _userAccessor.GetCurrentUserName();
                    entry.Entity.CreatedDate = _dateService.Now;
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate = _dateService.Now;
                    entry.Entity.ModifiedBy = _userAccessor.GetCurrentUserName();
                    break;
            }
        }
        return await _context.SaveChangesAsync() > 0;
    }
}