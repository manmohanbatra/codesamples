using Moq;
using ReadersApi.Controllers;
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
        IUnitOfWork unitOfWork;

        public UserController_Tests()
        {
            //Arrange
            var repomoq = new Mock<IUserRepo>();
            repomoq.Setup(x => x.Find(It.IsAny<Expression<Func<User, bool>>>())).Returns(new List<User>() { new User() });
            var repo = repomoq.Object;

            var uowmoq = new Mock<IUnitOfWork>();
            uowmoq.Setup(x => x.UserRepo).Returns(repo);
            unitOfWork = uowmoq.Object;
        }

        [Fact]
        public void GetUsers_Success()
        {
            //Arrange
            var userController = new UserController(unitOfWork);

            //Act
            var users = userController.GetUsers().ToList();

            //Assert
            Assert.True(users.Count > 0);
        }
    }
}
