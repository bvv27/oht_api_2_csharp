using System;
using System.Net;
using System.Security.Policy;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICancelProjectProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId);
    }
    public class CancelProjectProvider : ICancelProjectProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "?public_key={0}&secret_key={1}"
                    , publicKey, secretKey);

                return client.UploadString(web, "DELETE", "");
            }
        }
    }

    partial class Ohtapi
    {
        public ICancelProjectProvider CancelProjectProvider;
        /// <summary>
        /// Prevent a project from being worked on by a linguist Constraints: Available only before actual translation starts
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public CancelProjectResult CancelProject(string projectId)
        {
            var r = new CancelProjectResult();
            try
            {
                if (CancelProjectProvider == null)
                    CancelProjectProvider = new CancelProjectProvider();
                var json = CancelProjectProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId);
                r = JsonConvert.DeserializeObject<CancelProjectResult>(json);
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct CancelProjectResult
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
