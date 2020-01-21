using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadersApi.Providers
{
    public interface IReaderRepo : IRepository<Reader>
    {
    }

    public class ReaderRepo : Repository<Reader>, IReaderRepo
    {
        public ReaderRepo(MyContext ctx) : base(ctx)
        {
        }
    }
}