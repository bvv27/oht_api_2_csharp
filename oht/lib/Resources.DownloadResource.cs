using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public DownloadResourceResult DownloadResource(string resourceUuid, int projectId = 0)
        {
            var r = new DownloadResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/resources/" + resourceUuid + "/download?secret_key={0}&public_key={1}&project_id={2}", KeySecret, KeyPublic, projectId);
                    r.File = client.DownloadData(web);
                    r.Status.Code = 0;
                    r.Status.Msg = "ok";
                    
                }
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
        
        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
}
