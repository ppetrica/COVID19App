using Microsoft.VisualStudio.TestTools.UnitTesting;
using network;


namespace test_network
{
    [TestClass]
    public class TestConnection
    {
        [TestMethod]
        public void TestOnline()
        {
            Assert.AreEqual(true, InternetConnection.isConnectionAvailable());
            Assert.AreEqual(true, InternetConnection.isConnectionAvailable(InternetConnection.DefaultTimeout,
                "https://www.newtonsoft.com/json"));
        }

        [TestMethod]
        [Ignore]
        public void TestOffline()
        {
            Assert.AreEqual(false, InternetConnection.isConnectionAvailable());
        }
    }
}
