using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests GetProjectDetails")]
    public class GetProjectDetails
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"project_id\":\"808128\",\"project_type\":\"Translation\",\"project_status\":\"Being translated\",\"project_status_code\":\"signed\",\"source_language\":\"en-us\",\"target_language\":\"ru-ru\",\"resources\":{\"sources\":[\"rsc-560e7ea4650793-27822858\"],\"translations\":[\"rsc-561fd9e3b30651-52344271\"],\"proofs\":\"\",\"transcriptions\":\"\"},\"wordcount\":\"3\",\"custom\":\"\",\"resource_binding\":{\"rsc-560e7ea4650793-27822858\":[\"rsc-561fd9e3b30651-52344271\"],\"rsc-561fd9e3b30651-52344271\":null},\"linguist_uuid\":\"70f6df63-9359-4f5b-a7c2-2483123a269a\"},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests GetProjectDetails")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.GetProjectDetails("808128");

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.GetProjectDetails("808128");

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<IGetProjectDetailsProvider>();
            ohtapi.GetProjectDetailsProvider = provider;
            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);

            result = ohtapi.GetProjectDetails(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.GetProjectDetails(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.GetProjectDetails(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
