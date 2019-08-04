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

            var test2 = await Instance.BPMSTaskFunctions.GetBPMSApprovals();
            Assert.IsTrue(test2.Count > 0);

        }
    }
}
