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
        /// <summary>
        /// Open a new translation project
        /// </summary>
        /// <param name="sourceLanguage">See Language Codes</param>
        /// <param name="targetLanguage">See Language Codes</param>
        /// <param name="sources">Comma separated list of Resource UUIDs</param>
        /// <param name="expertise">[Optional] See Expertise Codes</param>
        /// <param name="wordcount">[Optional] If empty use automatic counting</param>
        /// <param name="notes">[Optional] Text note that will be shown to translator regarding the newly project</param>
        /// <param name="callbackUrl">[Optional] See Callbacks section</param>
        /// <param name="name">[Optional] Name your project. If empty, your project will be named automatically.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Unique id of the newly project created
        /// </summary>
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        /// <summary>
        /// Total word count of the newly project
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        /// <summary>
        /// Total credit worth of the newly project
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
