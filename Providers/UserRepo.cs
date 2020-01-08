using System;
using System.Collections.Generic;
using System.Linq;

namespace ReadersApi.Providers
{
    public interface IUserRepo : IRepository<User>
    {
    }

    public class UserRepo : Repository<User>, IUserRepo
    {
        MyContext context;
        
        public UserRepo(MyContext ctx) : base(ctx)
        {
            this.context = ctx;
        }
    }
}