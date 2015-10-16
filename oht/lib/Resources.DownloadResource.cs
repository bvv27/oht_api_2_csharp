using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IDownloadResourceProvider
    {
        byte[] Get(string url, WebProxy proxy, string publicKey, string secretKey, string resourceUuid, int projectId = 0);
    }
    public class DownloadResourceProvider : IDownloadResourceProvider
    {
        public byte[] Get(string url, WebProxy proxy, string publicKey, string secretKey, string resourceUuid, int projectId = 0)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/resources/" + resourceUuid + "/download?public_key={0}&secret_key={1}&project_id={2}", publicKey, secretKey, projectId);
                return client.DownloadData(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IDownloadResourceProvider DownloadResourceProvider;
        /// <summary>
        /// File is downloaded with original name, text content is downloaded with filename: “oht_<resource_uuid>.txt” 
        /// </summary>
        /// <param name="resourceUuid"></param>
        /// <param name="projectId">(optional) Project ID, needed when requesting a resource that was uploaded by another user - e.g. as a project’s translation</param>
        /// <returns></returns>
        public DownloadResourceResult DownloadResource(string resourceUuid, int projectId = 0)
        {
            var r = new DownloadResourceResult();
            try
            {
                if (DownloadResourceProvider == null)
                    DownloadResourceProvider = new DownloadResourceProvider();
                byte[] file = DownloadResourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, resourceUuid, projectId);


                var json = System.Text.Encoding.UTF8.GetString(file);
                if (json.StartsWith("{\"status\":{\""))
                {
                    return JsonConvert.DeserializeObject<DownloadResourceResult>(json);
                }
                r.File = file;
                r.Status.Code = 0;
                r.Status.Msg = "ok";
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }
    }

    public struct DownloadResourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "file")]
        public byte[] File;
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
