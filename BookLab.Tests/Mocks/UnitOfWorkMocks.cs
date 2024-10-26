using BookLab.Domain.Interfaces;
using Moq;
using System.Data;

namespace BookLab.Tests.Mocks;

public class UnitOfWorkMocks
{
    public Mock<IUnitOfWork> UnitOfWork;

    public UnitOfWorkMocks()
    {
        UnitOfWork = new Mock<IUnitOfWork>();

        var mockDbTransaction = new Mock<IDbTransaction>();

        UnitOfWork.Setup(x => x.BeginTransaction())
            .Returns(() =>
            {
                return mockDbTransaction.Object;
            });
    }
}