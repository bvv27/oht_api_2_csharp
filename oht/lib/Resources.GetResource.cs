﻿using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IGetResourceProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resourceUuid, int projectId = 0, string fetch = "");
    }
    public class GetResourceProvider : IGetResourceProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string resourceUuid, int projectId = 0, string fetch = "")
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/resources/" + resourceUuid + "?public_key={0}&secret_key={1}&project_id={2}&fetch={3}", publicKey, secretKey, projectId, fetch);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IGetResourceProvider GetResourceProvider;
        public GetResourceResult GetResource(string resourceUuid, int projectId=0, string fetch="")
        {
            var r = new GetResourceResult();
            try
            {
                if (GetResourceProvider == null)
                    GetResourceProvider = new GetResourceProvider();
                var json = GetResourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, resourceUuid, projectId, fetch);
                r = JsonConvert.DeserializeObject<GetResourceResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
            }
            catch (Exception err)
            {
                r.Status.Code = -1;
                r.Status.Msg = err.Message;
            }
            return r;
        }

    }

    public struct GetResourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetResourceResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetResourceResultType
    {
        [JsonProperty(PropertyName = "type")]
        public string Type;
        [JsonProperty(PropertyName = "length")]
        public Int64 Length;
        [JsonProperty(PropertyName = "file_name")]
        public string FileName;
        [JsonProperty(PropertyName = "file_mime")]
        public string FileMime;
        [JsonProperty(PropertyName = "download_url")]
        public string DownloadUrl;
        [JsonProperty(PropertyName = "content")]
        public string Content;
    }
}
