using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadersApi.Providers;

namespace ReadersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        IAuthRepo _repo;
        IUnitOfWork _readerRepo;
        
        public ReaderController(IAuthRepo repo, IUnitOfWork readerRepo)
        {
            _repo = repo;
            _readerRepo = readerRepo;
        }

        // [Authorize("ShouldBeAnAdmin")]
        [Authorize]
        [Route("all")]
        [HttpGet]
        public IEnumerable<Reader> GetReaders()
        {
            return _readerRepo.ReaderRepo.Find(x => x.Id != 0);
        }

        [Route("token")]
        [HttpPost]
        public AuthResult Token([FromBody]LoginModel credentials)
        {
            return _repo.Authenticate(credentials);
        }
    }
}