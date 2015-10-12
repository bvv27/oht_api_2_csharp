using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests TranslateViaMachineTranslation")]
    public class TranslateViaMachineTranslationTest
    {
        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"TranslatedText\":\"\\u0437\\u0435\\u043b\\u0435\\u043d\\u044b\\u0439 \\u043f\\u0430\\u0440\\u0443\\u0441\"},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":103,\"msg\":\"request validation failed\"},\"results\":[],\"errors\":[\"source_language\"]}";
        [Test, Description("Tests TranslateViaMachineTranslation")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.TranslateViaMachineTranslation("en-us","ru-ru","text");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            result = ohtapi.TranslateViaMachineTranslation("", "ru-ru", "text");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ITranslateViaMachineTranslationProvider>();
            ohtapi.TranslateViaMachineTranslationProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.TranslateViaMachineTranslation(string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.TranslateViaMachineTranslation(string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.TranslateViaMachineTranslation(string.Empty, string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);
        }
    }
}
