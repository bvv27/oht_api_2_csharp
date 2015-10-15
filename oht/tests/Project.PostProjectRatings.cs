using System;
using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests PostProjectRatings")]
    public class PostProjectRatingsTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":[],\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests PostProjectRatings")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            ohtapi = new Ohtapi("","", true);
            var result = ohtapi.PostProjectRatings(string.Empty, string.Empty, 0);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<IPostProjectRatingsProvider>();
            ohtapi.PostProjectRatingsProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, 0).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.PostProjectRatings(string.Empty, string.Empty, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, 0).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.PostProjectRatings(string.Empty, string.Empty, 0);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty, string.Empty, 0).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.PostProjectRatings(string.Empty, string.Empty, 0);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
