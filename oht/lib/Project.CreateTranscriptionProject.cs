using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateTranscriptionProjectProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            , string sources, string length = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null);
    }
    public class CreateTranscriptionProjectProvider : ICreateTranscriptionProjectProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage
            , string sources, string length = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/transcription?public_key={0}&secret_key={1}&source_language={2}&sources={3}&length={4}"
                    , publicKey, secretKey, sourceLanguage, sources, length);
                var values = new System.Collections.Specialized.NameValueCollection
                    {
                        {"notes", notes},
                        {"callback_url", callbackUrl},
                        {"name", name}
                    };

                if (custom != null)
                {
                    for(var i = 0; i < custom.Length; i++)
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
        public ICreateTranscriptionProjectProvider CreateTranscriptionProjectProvider;
        /// <summary>
        /// Post a transcription project
        /// </summary>
        /// <param name="sourceLanguage">See Language Codes</param>
        /// <param name="sources">Comma separated list of Resource UUIDs</param>
        /// <param name="length">[Optional] Integer of seconds, if empty use automatic counting</param>
        /// <param name="notes">[Optional] Text note that will be shown to translator regarding the newly project</param>
        /// <param name="callbackUrl">[Optional] See Callbacks section</param>
        /// <param name="name">[Optional] Name your project. If empty, your project will be named automatically.</param>
        /// <param name="custom">[Optional]</param>
        /// <returns></returns>
        public CreateTranscriptionProjectResult CreateTranscriptionProject(string sourceLanguage
            , string sources, string length = "", string notes = "", string callbackUrl = "", string name = "", string[] custom = null)
        {
            var r = new CreateTranscriptionProjectResult();
            try
            {

                if (CreateTranscriptionProjectProvider == null)
                    CreateTranscriptionProjectProvider = new CreateTranscriptionProjectProvider();
                var json = CreateTranscriptionProjectProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage, sources, length, notes, callbackUrl, name);
                r = JsonConvert.DeserializeObject<CreateTranscriptionProjectResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct CreateTranscriptionProjectResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateTranscriptionProjectType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateTranscriptionProjectType
    {
        /// <summary>
        /// Unique id of the newly project created
        /// </summary>
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        /// <summary>
        /// Total length (in seconds) of the newly transcription project
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Length;
        /// <summary>
        /// Total credit worth of the newly project
        /// </summary>
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
