using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IPostProjectRatingsProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string type, int rate, string remarks = "");
    }
    public class PostProjectRatingsProvider : IPostProjectRatingsProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string type, int rate, string remarks = "")
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "/rating?public_key={0}&secret_key={1}&type={2}&rate={3}&remarks={4}", publicKey, secretKey, type, rate, remarks);

                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IPostProjectRatingsProvider PostProjectRatingsProvider;
        /// <summary>
        /// Submit the rating for the quality of the translation and service
        /// </summary>
        /// <param name="projectId">Unique id of the newly project created</param>
        /// <param name="type">Customer|Service</param>
        /// <param name="rate">Rating of project (1 - being the lowest; 10 - being the highest)</param>
        /// <param name="remarks">Remark left with the rating</param>
        /// <returns></returns>
        public PostProjectRatingsResult PostProjectRatings(string projectId, string type,int rate, string remarks="")
        {
            var r = new PostProjectRatingsResult();
            try
            {
                if (PostProjectRatingsProvider == null)
                    PostProjectRatingsProvider = new PostProjectRatingsProvider();
                var json = PostProjectRatingsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId, type, rate, remarks);
                r = JsonConvert.DeserializeObject<PostProjectRatingsResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }


    public struct PostProjectRatingsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }

}
