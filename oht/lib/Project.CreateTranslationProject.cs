using System;
using System.Net;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateTranslationProjectProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string targetLanguage
            , string sources, string expertise, string wordcount = "", string notes = "", string callbackUrl = "", string name = "");
    }
    public class CreateTranslationProjectProvider : ICreateTranslationProjectProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string targetLanguage
            , string sources, string expertise, string wordcount = "", string notes = "", string callbackUrl = "", string name = "")
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/translation?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&expertise={5}"
                    , publicKey, secretKey, sourceLanguage, targetLanguage, sources, expertise);

                var values = new System.Collections.Specialized.NameValueCollection
                    {
                        {"wordcount", wordcount},
                        {"notes", notes},
                        {"callback_url", callbackUrl},
                        {"name", name}
                    };

                return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }
    partial class Ohtapi
    {
        public ICreateTranslationProjectProvider CreateTranslationProjectProvider;

        public CreateTranslationProjectResult CreateTranslationProject(string sourceLanguage, string targetLanguage
            , string sources, string expertise, string wordcount = "", string notes = "", string callbackUrl = "", string name = "")
        {
            var r = new CreateTranslationProjectResult();
            try
            {
                if (CreateTranslationProjectProvider == null)
                    CreateTranslationProjectProvider = new CreateTranslationProjectProvider();
                var json = CreateTranslationProjectProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage, targetLanguage, sources, expertise, wordcount, notes, callbackUrl, name);
                r = JsonConvert.DeserializeObject<CreateTranslationProjectResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }


    public struct CreateTranslationProjectResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateTranslationProjectType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateTranslationProjectType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
