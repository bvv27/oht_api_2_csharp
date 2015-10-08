using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests Account")]
    public class AccountTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":{\"credits\":\"100000.0000\",\"account_id\":\"640\",\"account_username\":\"bvv27\",\"role\":\"customer\"},\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests account")]
        public void TestAccount()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            AccountResult accountResult = ohtapi.Account();

            Assert.IsNotNull(accountResult);
            Assert.AreEqual(0, accountResult.Status.Code);

            ohtapi = new Ohtapi("","", true);
            accountResult = ohtapi.Account();

            Assert.IsNotNull(accountResult);
            Assert.AreNotEqual(0, accountResult.Status.Code);
            Assert.AreNotEqual(-1, accountResult.Status.Code);

            var accountProvider = Substitute.For<IAccountProvider>();
            ohtapi.AccountProvider = accountProvider;
            accountProvider.Get(string.Empty, null, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);
            
            accountResult = ohtapi.Account();

            Assert.IsNotNull(accountResult);
            Assert.AreEqual(0, accountResult.Status.Code);

            accountProvider.Get(string.Empty, null, string.Empty, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            accountResult = ohtapi.Account();

            Assert.IsNotNull(accountResult);
            Assert.AreNotEqual(0, accountResult.Status.Code);
            Assert.AreNotEqual(-1, accountResult.Status.Code);

            accountProvider.Get(string.Empty, null, string.Empty, string.Empty).ReturnsForAnyArgs(string.Empty);
            accountResult = ohtapi.Account();

            Assert.IsNotNull(accountResult);
            Assert.AreEqual(-1, accountResult.Status.Code);

        }
    }
}
