using BPMSApi.Model.BPMSTask;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            var test = await Instance.BPMSTaskFunctions.UpdateBPMSTask(new UpdateBPMSTaskRequest
            {
                Approved = false,
                Approver = "Nelson Coelho",
                RejectionReason = "Expired."
            });
            Assert.IsTrue(test.Data.Count > 0);
        }
    }
}
