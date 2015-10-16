using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IGetWordCountProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resources);
    }
    public class GetWordCountProvider : IGetWordCountProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resources)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/tools/wordcount?public_key={0}&secret_key={1}&resources={2}"
                    , publicKey, secretKey, resources);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IGetWordCountProvider GetWordCountProvider;
        /// <summary>
        /// Get the word count of provided resources
        /// </summary>
        /// <param name="resources">a comma (,) separated list of resource_uuid</param>
        /// <returns></returns>
        public GetWordCountResult GetWordCount(string resources)
        {
            var r = new GetWordCountResult();
            try
            {
                if (GetWordCountProvider == null)
                    GetWordCountProvider = new GetWordCountProvider();
                var json = GetWordCountProvider.Get(Url, _proxy, KeyPublic, KeySecret, resources);
                r = JsonConvert.DeserializeObject<GetWordCountResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct GetWordCountResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetWordCountType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetWordCountType
    {
        /// <summary>
        /// Array of results per resource
        /// </summary>
        [JsonProperty(PropertyName = "resources")]
        public GetWordCountResources[] Resources;
        /// <summary>
        /// total words count
        /// </summary>
        [JsonProperty(PropertyName = "total")]
        public GetWordCountTotal Total;

    }

    public struct GetWordCountResources
    {
        /// <summary>
        /// UUID of the resource in list
        /// </summary>
        [JsonProperty(PropertyName = "resource")]
        public string Resource;
        /// <summary>
        /// total resource word count
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
    }
    public struct GetWordCountTotal
    {
        /// <summary>
        /// total words count
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
    }
}
