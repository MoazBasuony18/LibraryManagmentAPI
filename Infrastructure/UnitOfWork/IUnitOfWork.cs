using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.Repository;

namespace LibraryManagmentAPI.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Checkout> Checkouts { get; }
        IRepository<User> Users { get; }
        Task Save();
    }
}
