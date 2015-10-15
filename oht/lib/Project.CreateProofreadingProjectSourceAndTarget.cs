using System;
using System.Net;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateProofreadingProjectSourceAndTargetProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            ,string targetLanguage , string sources, string translations, string wordcount="", string notes="", string callbackUrl="", string name="");
    }
    public class CreateProofreadingProjectSourceAndTargetProvider : ICreateProofreadingProjectSourceAndTargetProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            , string targetLanguage, string sources, string translations, string wordcount = "", string notes = "", string callbackUrl = "", string name = "")
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/proof-translated?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&translations={5}&wordcount={6}&callback_url={7}&name={8}"
                    , publicKey, secretKey, sourceLanguage, targetLanguage, sources, translations, wordcount, callbackUrl, name);
                var values = new System.Collections.Specialized.NameValueCollection { { "notes", notes } };
                return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }

    partial class Ohtapi
    {
        public ICreateProofreadingProjectSourceAndTargetProvider CreateProofreadingProjectSourceAndTargetProvider;
        public CreateProofreadingProjectSourceAndTargetResult CreateProofreadingProjectSourceAndTarget(string sourceLanguage
            ,string targetLanguage , string sources, string translations, string wordcount="", string notes="", string callbackUrl="", string name="")
        {
            var r = new CreateProofreadingProjectSourceAndTargetResult();
            try
            {
                if (CreateProofreadingProjectSourceAndTargetProvider == null)
                    CreateProofreadingProjectSourceAndTargetProvider = new CreateProofreadingProjectSourceAndTargetProvider();
                var json = CreateProofreadingProjectSourceAndTargetProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage,targetLanguage , sources, translations, wordcount, notes, callbackUrl, name);
                r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceAndTargetResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct CreateProofreadingProjectSourceAndTargetResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateProofreadingProjectSourceAndTargetType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceAndTargetType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
