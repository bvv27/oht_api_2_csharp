using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests SupportedExpertises")]
    public class SupportedExpertisesTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":[{\"name\":\"Automotive\\/Aerospace\",\"code\":\"326\"},{\"name\":\"IT\",\"code\":\"328\"},{\"name\":\"Legal\",\"code\":\"329\"},{\"name\":\"Marketing\\/Consumer\",\"code\":\"330\"},{\"name\":\"Media\\/Entertainment\",\"code\":\"331\"}],\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":105,\"msg\":\"action is forbidden because of certain dependencies\"},\"results\":[],\"errors\":[\"sources\"]}";
        [Test, Description("Tests SupportedExpertises")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.SupportedExpertises("en-us", "ru-ru");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.SupportedExpertises("en-us", "ru-ru");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ISupportedExpertisesProvider>();
            ohtapi.SupportedExpertisesProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.SupportedExpertises(string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.SupportedExpertises(string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.SupportedExpertises(string.Empty, string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
