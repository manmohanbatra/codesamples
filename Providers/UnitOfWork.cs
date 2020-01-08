namespace ReadersApi.Providers
{
    public interface IUnitOfWork
    {
        IReaderRepo ReaderRepo { get; }
        IUserRepo UserRepo { get; }
        void Save();
    }
    
    public class UnitOfWork : IUnitOfWork
    {
        private MyContext context;

        public UnitOfWork(MyContext context)
        {
            this.context = context;
        }

        public IReaderRepo ReaderRepo => new ReaderRepo(context);
        public IUserRepo UserRepo => new UserRepo(context);

        public void Save()
        {
            this.context.SaveChanges();
        }
    }
}