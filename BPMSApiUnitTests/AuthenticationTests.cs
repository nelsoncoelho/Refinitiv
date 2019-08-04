using BPMSApi.Model.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BPMSApiUnitTests
{
    [TestClass]
    public class AuthenticationTests: TestBase
    {
        [TestMethod]
        public async Task AuthenticationTest()
        {
            var user = new User
            {
                Username = "test",
                Password = "test",
                Name = "This is a test"
            };

            // Verifies that registration works.
            var registration = await Instance.AuthenticationFunctions.Register(user);
            Assert.IsTrue(registration);

            // Verifies that duplicate users cannot exist.
            registration = await Instance.AuthenticationFunctions.Register(user);
            Assert.IsFalse(registration);

            // Verifies that log in with a correct user works.
            var login = await Instance.AuthenticationFunctions.Login(user);
            Assert.IsNotNull(login);
            Assert.IsTrue(login == user.Name);

            // Verifies that log in with an incorrect user does not work.
            user.Username = "fake";
            login = await Instance.AuthenticationFunctions.Login(user);
            Assert.IsNull(login);
        }
    }
}
