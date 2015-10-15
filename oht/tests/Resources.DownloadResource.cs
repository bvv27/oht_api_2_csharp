using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests DownloadResource")]
    public class DownloadResourceTest
    {
        [Test, Description("Tests DownloadResource")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.DownloadResource("rsc-560e7ea4650793-27822858");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.DownloadResource("21312312312312");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);
        }
    }
}
