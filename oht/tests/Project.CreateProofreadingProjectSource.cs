using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests CreateProofreadingProjectSource")]
    public class CreateProofreadingProjectSourceTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"project_id\":\"807963\",\"wordcount\":3,\"credits\":23.7},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests CreateProofreadingProjectSource")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.CreateProofreadingProjectSource("en-us", "rsc-560e7ea4650793-27822858", "", "", "marketing-consumer-media","","name12");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("", "", true);
            result = ohtapi.CreateProofreadingProjectSource("en-us", "rsc-560e7ea4650793-27822858", "", "", "marketing-consumer-media", "", "name12");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ICreateProofreadingProjectSourceProvider>();
            ohtapi.CreateProofreadingProjectSourceProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.CreateProofreadingProjectSource(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.CreateProofreadingProjectSource(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.CreateProofreadingProjectSource(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
