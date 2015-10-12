using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests SupportedLanguagePairs")]
    public class SupportedLanguagePairsTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":[{\"source\":{\"name\":\"Arabic\",\"code\":\"ar-sa\"},\"targets\":[{\"name\":\"English\",\"code\":\"en-us\",\"availability\":\"low\"}]},{\"source\":{\"name\":\"Dutch\",\"code\":\"nl-nl\"},\"targets\":[{\"name\":\"French\",\"code\":\"fr-fr\",\"availability\":\"low\"}]},{\"source\":{\"name\":\"English\",\"code\":\"en-us\"},\"targets\":[{\"name\":\"Dutch\",\"code\":\"nl-nl\",\"availability\":\"low\"},{\"name\":\"French\",\"code\":\"fr-fr\",\"availability\":\"medium\"},{\"name\":\"German\",\"code\":\"de-de\",\"availability\":\"low\"},{\"name\":\"Hebrew\",\"code\":\"he-il\",\"availability\":\"low\"},{\"name\":\"Norwegian\",\"code\":\"no-no\",\"availability\":\"low\"},{\"name\":\"Russian\",\"code\":\"ru-ru\",\"availability\":\"low\"},{\"name\":\"Spanish\",\"code\":\"es-es\",\"availability\":\"low\"}]},{\"source\":{\"name\":\"French\",\"code\":\"fr-fr\"},\"targets\":[{\"name\":\"English\",\"code\":\"en-us\",\"availability\":\"low\"}]},{\"source\":{\"name\":\"German\",\"code\":\"de-de\"},\"targets\":[{\"name\":\"English\",\"code\":\"en-us\",\"availability\":\"low\"},{\"name\":\"French\",\"code\":\"fr-fr\",\"availability\":\"low\"},{\"name\":\"Italian\",\"code\":\"it-it\",\"availability\":\"low\"}]}],\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests SupportedLanguagePairs")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.SupportedLanguagePairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.SupportedLanguagePairs();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ISupportedLanguagePairsProvider>();
            ohtapi.SupportedLanguagePairsProvider = provider;
            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.SupportedLanguagePairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.SupportedLanguagePairs();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.SupportedLanguagePairs();

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
