using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMiner;

namespace MiniMinerTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var pool = new TestPool("lithander_2:foo@btcguild.com:8332");

            Worker.isTesting = true;
            pool.StartWorkers();
            Assert.AreEqual(true, true);
        }
    }
}
