using LibraryManagmentAPI.Domain.Entities;
using LibraryManagmentAPI.Infrastructure.Data;
using LibraryManagmentAPI.Infrastructure.Repository;

namespace LibraryManagmentAPI.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IRepository<Book> books;
        private IRepository<Checkout> checkouts;
        private IRepository<User> users;
        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
        }
        public IRepository<Book> Books => books ?? new Repository<Book>(context);

        public IRepository<Checkout> Checkouts => checkouts?? new Repository<Checkout>(context);
        public IRepository<User> Users => users?? new Repository<User>(context);

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
