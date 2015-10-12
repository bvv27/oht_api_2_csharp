using NSubstitute;
using NUnit.Framework;
using oht.lib;

namespace oht.tests
{
    [TestFixture, Description("Tests Discover.SupportedLanguages")]
    public class SupportedLanguagesTest
    {

        private const string ExpectedJsonResultOk = "{\"status\":{\"code\":0,\"msg\":\"ok\"},\"results\":[{\"name\":\"Afrikaans\",\"code\":\"af\"},{\"name\":\"Albanian\",\"code\":\"sq-al\"},{\"name\":\"Arabic\",\"code\":\"ar-sa\"},{\"name\":\"Armenian\",\"code\":\"hy-am\"},{\"name\":\"Azerbaijani\",\"code\":\"az-az\"},{\"name\":\"Bengali\",\"code\":\"bn-bd\"},{\"name\":\"Bosnian\",\"code\":\"bs-ba\"},{\"name\":\"Bulgarian\",\"code\":\"bg-bg\"},{\"name\":\"Catalan\",\"code\":\"ca-es\"},{\"name\":\"Chinese (Cantonese)\",\"code\":\"zh-cn-yue\"},{\"name\":\"Chinese Mandarin Simplified\",\"code\":\"zh-cn-cmn-s\"},{\"name\":\"Chinese Mandarin Traditional\",\"code\":\"zh-cn-cmn-t\"},{\"name\":\"Croatian\",\"code\":\"hr-hr\"},{\"name\":\"Czech\",\"code\":\"cs-cz\"},{\"name\":\"Danish\",\"code\":\"da\"},{\"name\":\"Dari\",\"code\":\"fa-af\"},{\"name\":\"Dutch\",\"code\":\"nl-nl\"},{\"name\":\"English\",\"code\":\"en-us\"},{\"name\":\"Estonian\",\"code\":\"et-ee\"},{\"name\":\"Finnish\",\"code\":\"fi-fi\"},{\"name\":\"Flemish (Belgian)\",\"code\":\"fl-be\"},{\"name\":\"French\",\"code\":\"fr-fr\"},{\"name\":\"French (Canadian)\",\"code\":\"fr-ca\"},{\"name\":\"Georgian\",\"code\":\"ka-ge\"},{\"name\":\"German\",\"code\":\"de-de\"},{\"name\":\"Greek\",\"code\":\"el-gr\"},{\"name\":\"Gujarati\",\"code\":\"gu-in\"},{\"name\":\"Haitian Creole\",\"code\":\"ht\"},{\"name\":\"Hebrew\",\"code\":\"he-il\"},{\"name\":\"Hindi\",\"code\":\"hi-in\"},{\"name\":\"Hungarian\",\"code\":\"hu-hu\"},{\"name\":\"Icelandic\",\"code\":\"is-is\"},{\"name\":\"Indonesian\",\"code\":\"id-id\"},{\"name\":\"Italian\",\"code\":\"it-it\"},{\"name\":\"Japanese\",\"code\":\"ja-jp\"},{\"name\":\"Kazakh\",\"code\":\"kk-kz\"},{\"name\":\"Khmer\",\"code\":\"km-kh\"},{\"name\":\"Korean\",\"code\":\"ko-kp\"},{\"name\":\"Kurdish\",\"code\":\"ku-tr\"},{\"name\":\"Latvian\",\"code\":\"lv-lv\"},{\"name\":\"Lithuanian\",\"code\":\"lt-lt\"},{\"name\":\"Macedonian\",\"code\":\"mk-mk\"},{\"name\":\"Malay\",\"code\":\"ms-my\"},{\"name\":\"Marathi\",\"code\":\"mr-in\"},{\"name\":\"Norwegian\",\"code\":\"no-no\"},{\"name\":\"Panjabi\",\"code\":\"pa-in\"},{\"name\":\"Pashto\",\"code\":\"ps\"},{\"name\":\"Persian\",\"code\":\"fa-ir\"},{\"name\":\"Polish\",\"code\":\"pl-pl\"},{\"name\":\"Portuguese (Brazil)\",\"code\":\"pt-br\"},{\"name\":\"Portuguese (Portugal)\",\"code\":\"pt-pt\"},{\"name\":\"Romanian\",\"code\":\"ro-ro\"},{\"name\":\"Russian\",\"code\":\"ru-ru\"},{\"name\":\"Sanskrit\",\"code\":\"sa-in\"},{\"name\":\"Serbian\",\"code\":\"sr-rs\"},{\"name\":\"Slovak\",\"code\":\"sk-sk\"},{\"name\":\"Slovenian (Slovene)\",\"code\":\"sl-si\"},{\"name\":\"Spanish\",\"code\":\"es-es\"},{\"name\":\"Spanish (Latin-America)\",\"code\":\"es-ar\"},{\"name\":\"Swedish\",\"code\":\"sv-se\"},{\"name\":\"Tagalog\",\"code\":\"tl-ph\"},{\"name\":\"Tamil\",\"code\":\"ta-in\"},{\"name\":\"Thai\",\"code\":\"th-th\"},{\"name\":\"Turkish\",\"code\":\"tr-tr\"},{\"name\":\"Ukrainian\",\"code\":\"uk-ua\"},{\"name\":\"Urdu\",\"code\":\"ur-pk\"},{\"name\":\"Uzbek\",\"code\":\"uz-uz\"},{\"name\":\"Vietnamese\",\"code\":\"vi-vn\"}],\"errors\":[]}";
        private const string ExpectedJsonResultErr = "{\"status\":{\"code\":101,\"msg\":\"unauthorized request - please include your public key \\/ account id and api secret key\"},\"results\":[],\"errors\":[]}";
        [Test, Description("Tests SupportedLanguages")]
        public void Test()
        {
            Ohtapi ohtapi = new Ohtapi(Tools.TestPublicKey, Tools.TestSecretKey, true);
            Assert.IsNotNull(ohtapi);

            var result = ohtapi.SupportedLanguages();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            ohtapi = new Ohtapi("","", true);
            result = ohtapi.SupportedLanguages();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            var provider = Substitute.For<ISupportedLanguagesProvider>();
            ohtapi.SupportedLanguagesProvider = provider;
            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultOk);
            
            result = ohtapi.SupportedLanguages();

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(ExpectedJsonResultErr);
            result = ohtapi.SupportedLanguages();

            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Status.Code);
            Assert.AreNotEqual(-1, result.Status.Code);

            provider.Get(string.Empty, null, string.Empty).ReturnsForAnyArgs(string.Empty);
            result = ohtapi.SupportedLanguages();

            Assert.IsNotNull(result);
            Assert.AreEqual(-1, result.Status.Code);

        }
    }
}
