using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IRetrieveProjectRatingsProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId);
    }
    public class RetrieveProjectRatingsProvider : IRetrieveProjectRatingsProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "/rating?public_key={0}&secret_key={1}", publicKey, secretKey);

                return client.DownloadString(web);
            }
        }
    }
    partial class Ohtapi
    {
        public IRetrieveProjectRatingsProvider RetrieveProjectRatingsProvider;
        /// <summary>
        /// Get the rating for the quality of the translation and service
        /// </summary>
        /// <param name="projectId">Unique id of the newly project created</param>
        /// <returns></returns>
        public RetrieveProjectRatingsResult RetrieveProjectRatings(string projectId)
        {
            var r = new RetrieveProjectRatingsResult();
            try
            {
                if (RetrieveProjectRatingsProvider == null)
                    RetrieveProjectRatingsProvider = new RetrieveProjectRatingsProvider();
                var json = RetrieveProjectRatingsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId);
                r = JsonConvert.DeserializeObject<RetrieveProjectRatingsResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct RetrieveProjectRatingsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public RetrieveProjectRatingsType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct RetrieveProjectRatingsType
    {
        /// <summary>
        /// Customer|Service
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type;
        /// <summary>
        /// Rating of project (1 - being the lowest; 10 - being the highest)
        /// </summary>
        [JsonProperty(PropertyName = "rate")]
        public int Rate;
        /// <summary>
        /// Remark left with the rating
        /// </summary>
        [JsonProperty(PropertyName = "remarks")]
        public string Remarks;
        /// <summary>
        /// Date and time of last update to the rating
        /// </summary>
        [JsonProperty(PropertyName = "date")]
        public string Date;
    }


}
