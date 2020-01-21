// using System.Collections.Generic;
// using System.Linq;

// namespace ReadersApi.Providers.Adapters
// {
//     public class ObjectReaderAdapter : IUserRepo
//     {
//         IReaderRepo repo;
//         public ObjectReaderAdapter(IReaderRepo repo)
//         {
//             this.repo = repo;
//         }

//         public List<User> GetUsers()
//         {
//             var readers = repo.GetReaders();
//             var users = ReaderStore.Users;
//             return users.Where(x => readers.Any(r => r.Id == x.Id)).ToList();
//         }
//     }

//     public class ClassReaderAdapter : ReaderRepo, IUserRepo
//     {
//         public List<User> GetUsers()
//         {
//             var readers = base.GetReaders();
//             var users = ReaderStore.Users;
//             return users.Where(x => readers.Any(r => r.Id == x.Id)).ToList();
//         }
//     }
// }