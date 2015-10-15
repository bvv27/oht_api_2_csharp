using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests CreateTranslationProject")]
    public class CreateTranslationProject
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"project_id\":\"808128\",\"wordcount\":3,\"credits\":23.7},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests CreateTranslationProject")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            ohtapi = new Ohtapi("", "", true);
            var result = ohtapi.CreateTranslationProject("en-us", "ru-ru", "rsc-560e7ea4650793-27822858", "rsc-560ea011277cb3-18000700");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ICreateTranslationProjectProvider>();
            ohtapi.CreateTranslationProjectProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.CreateTranslationProject(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.CreateTranslationProject(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.CreateTranslationProject(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
