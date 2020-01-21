using Moq;
using ReadersApi.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace Readers.Tests
{
    public class UserController_Tests
    {
        IUserRepo repo;
        IUnitOfWork unitOfWork;

        public UserController_Tests()
        {
            //Arrange
            var repomoq = new Mock<IUserRepo>();
            repomoq.Setup(x => x.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User>() { new User() });
            repo = repomoq.Object;

            var uowmoq = new Mock<IUnitOfWork>();
            uowmoq.Setup(x => x.UserRepo).Returns(repo);
            unitOfWork = uowmoq.Object;
        }

        [Fact]
        public void GetUsers_Success()
        {
            //Act
            var users = unitOfWork.UserRepo.Find(x => x.Id == 1).ToList();

            //Assert
            Assert.True(users.Count > 0);
        }
    }
}
