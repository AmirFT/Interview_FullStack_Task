using BackEnd.Application.Common.Interfaces;

namespace BackEnd.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
