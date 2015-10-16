using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateProofreadingProjectSourceProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string sources
            , string wordcount = "", string notes = "", string expertise = "", string callbackUrl = "", string name = "", string[] custom = null);
    }
    public class CreateProofreadingProjectSourceProvider : ICreateProofreadingProjectSourceProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string sources
            , string wordcount, string notes, string expertise, string callbackUrl, string name, string[] custom = null)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/proof-general?public_key={0}&secret_key={1}", publicKey, secretKey);
                var values = new System.Collections.Specialized.NameValueCollection
                {
                    {"source_language", sourceLanguage }
                    ,{"sources", sources }
                    ,{"wordcount", wordcount }
                    ,{"expertise", expertise }
                    ,{"callback_url", callbackUrl }
                    ,{ "notes", notes },{ "name", name }
                };
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
        public ICreateProofreadingProjectSourceProvider CreateProofreadingProjectSourceProvider;

        /// <summary>
        /// Create new proofreading project, same language
        /// </summary>
        /// <param name="sourceLanguage">See Language Codes</param>
        /// <param name="sources">Comma separated list of Resource UUIDs</param>
        /// <param name="wordcount">[Optional] If empty use automatic counting</param>
        /// <param name="notes">[Optional] Text note that will be shown to translator regarding the newly project</param>
        /// <param name="expertise">[Optional] See Expertise Codes</param>
        /// <param name="callbackUrl">[Optional] See Callbacks section</param>
        /// <param name="name">[Optional] Name your project. If empty, your project will be named automatically.</param>
        /// <param name="custom">[Optional]</param>
        /// <returns></returns>
        public CreateProofreadingProjectSourceResult CreateProofreadingProjectSource(string sourceLanguage
            , string sources, string wordcount, string notes, string expertise, string callbackUrl, string name, string[] custom = null)
        {
            var r = new CreateProofreadingProjectSourceResult();
            try
            {
                if (CreateProofreadingProjectSourceProvider == null)
                    CreateProofreadingProjectSourceProvider = new CreateProofreadingProjectSourceProvider();
                var json = CreateProofreadingProjectSourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage, sources, wordcount, notes, expertise, callbackUrl, name);
                r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct CreateProofreadingProjectSourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateProofreadingProjectSourceType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceType
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
