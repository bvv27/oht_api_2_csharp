using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IPostNewProjectCommentProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string content);
    }
    public class PostNewProjectCommentProvider : IPostNewProjectCommentProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId, string content)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "/comments?public_key={0}&secret_key={1}", publicKey, secretKey);

                var values = new System.Collections.Specialized.NameValueCollection { { "content", content } };
                return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }
    partial class Ohtapi
    {
        public IPostNewProjectCommentProvider PostNewProjectCommentProvider;
        /// <summary>
        /// Post a new project comment to the project page
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="content">content (text)</param>
        /// <returns></returns>
        public PostNewProjectCommentResult PostNewProjectComment(string projectId, string content)
        {
            var r = new PostNewProjectCommentResult();
            try
            {
                if (PostNewProjectCommentProvider == null)
                    PostNewProjectCommentProvider = new PostNewProjectCommentProvider();
                var json = PostNewProjectCommentProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId, content);
                r = JsonConvert.DeserializeObject<PostNewProjectCommentResult>(json);


            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct PostNewProjectCommentResult
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
