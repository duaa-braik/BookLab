using System.Data;

namespace BookLab.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();

    IDbTransaction BeginTransaction();
}
