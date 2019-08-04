using BPMSApi.Model.Authentication;
using BPMSApi.Model.BPMSTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BPMSApiUnitTests
{
    [TestClass]
    public class UnitTest1 : TestBase
    {
        [TestMethod]
        public async Task GetBPMSTasksTest()
        {
            var test = await Instance.BPMSTaskFunctions.GetBPMSTasks();
            Assert.IsTrue(test.Data.Count > 0);
        }

        [TestMethod]
        public async Task PutBPMSTasksTest()
        {
            var request = new UpdateBPMSTaskRequest
            {
                Id = 999,
                Approved = false,
                Approver = "Nelson Coelho",
                RejectionReason = "Expired."
            };
            var test = await Instance.BPMSTaskFunctions.UpdateBPMSTask(request);
            Assert.IsTrue(test.Count == 0);

            request.Id = 8;
            test = await Instance.BPMSTaskFunctions.UpdateBPMSTask(request);
            Assert.IsTrue(test.Count > 0);
        }

        [TestMethod]
        public async Task AuthenticationTest()
        {
            var user = new User
            {
                Username = "test",
                Password = "test",
                Name = "This is a test"
            };

            var registration = await Instance.AuthenticationFunctions.Register(user);
            Assert.IsTrue(registration);
            var login = await Instance.AuthenticationFunctions.Login(user);
            Assert.IsNotNull(login);
            Assert.IsTrue(login == user.Name);
        }
    }
}
