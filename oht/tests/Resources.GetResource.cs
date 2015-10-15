using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests GetResource")]
    public class GetResourceTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"type\":\"file\",\"length\":109,\"file_name\":\"1.txt\",\"file_mime\":\"file_mime\",\"download_url\":\"https:\\/\\/sandbox.onehourtranslation.com\\/api\\/2\\/resources\\/rsc-560e7ea4650793-27822858\\/download\"},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests GetResource")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.GetResource("rsc-560e7ea4650793-27822858", 0, "");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.GetResource("rsc-560e7ea4650793-27822858", 0, "");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<IGetResourceProvider>();
            ohtapi.GetResourceProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.GetResource(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.GetResource(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.GetResource(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
