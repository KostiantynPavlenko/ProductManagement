using ProductManagement.Domain.Interfaces;

namespace ProductManagement.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
