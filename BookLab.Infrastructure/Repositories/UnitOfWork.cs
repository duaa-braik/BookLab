using BookLab.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace BookLab.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly BookLabDbContext _context;

    public UnitOfWork(BookLabDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public IDbTransaction BeginTransaction()
    {
        var transaction =  _context.Database.BeginTransaction();

        return transaction.GetDbTransaction();
    }
}
