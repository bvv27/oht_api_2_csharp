using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateProofreadingProjectSourceAndTargetProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            , string targetLanguage, string sources, string translations, string wordcount = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null);
    }
    public class CreateProofreadingProjectSourceAndTargetProvider : ICreateProofreadingProjectSourceAndTargetProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            , string targetLanguage, string sources, string translations, string wordcount = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/proof-translated?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&translations={5}&wordcount={6}&callback_url={7}&name={8}"
                    , publicKey, secretKey, sourceLanguage, targetLanguage, sources, translations, wordcount, callbackUrl, name);
                var values = new System.Collections.Specialized.NameValueCollection { { "notes", notes } };
                if (custom != null)
                {
                    for (var i = 0; i < custom.Length; i++)
                    {
                        values.Add("custom" + i, custom[i]);
                    }
                }
                return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }

    partial class Ohtapi
    {
        public ICreateProofreadingProjectSourceAndTargetProvider CreateProofreadingProjectSourceAndTargetProvider;

        /// <summary>
        /// Create new proofreading project, Providing source and translation
        /// </summary>
        /// <param name="sourceLanguage">See Language Codes</param>
        /// <param name="targetLanguage">See Language Codes</param>
        /// <param name="sources">Comma separated list of Resource UUIDs</param>
        /// <param name="translations">Comma separated list of Resource UUIDs</param>
        /// <param name="wordcount">[Optional] If empty use automatic counting</param>
        /// <param name="notes">[Optional] Text note that will be shown to translator regarding the newly project</param>
        /// <param name="callbackUrl">[Optional] See Callbacks section</param>
        /// <param name="name">[Optional] Name your project. If empty, your project will be named automatically.</param>
        /// <param name="custom">[Optional]</param>
        /// <returns></returns>
        public CreateProofreadingProjectSourceAndTargetResult CreateProofreadingProjectSourceAndTarget(string sourceLanguage
            , string targetLanguage, string sources, string translations, string wordcount = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null)
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
