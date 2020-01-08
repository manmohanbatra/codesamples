using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReadersApi.Providers;

namespace ReadersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUnitOfWork repo;

        public UserController(IUnitOfWork repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("allreaders")]
        public IEnumerable<User> GetReaders()
        {
            return repo.UserRepo.Find(x => x.Id != 0);
        }
    }
}