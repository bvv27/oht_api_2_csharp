using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetResourceResult GetResource(string public_key, string secret_key, string resource_uuid, int project_id=0, string fetch="")
        {
            var r = new GetResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/resources/" + resource_uuid + "?secret_key={0}&public_key={1}&project_id={2}&fetch={3}", secret_key, public_key, project_id, fetch);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetResourceResult>(json);
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }

        public GetResourceResult GetResource(string resource_uuid, int project_id=0, string fetch="")
        {
            return GetResource(KeyPublic, KeySecret, resource_uuid, project_id, fetch);
        }
    }

    public struct GetResourceResult
    {

        public StatusType status;
        public GetResourceResultType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
    public struct GetResourceResultType
    {
        public string type;
        public Int64 length;
        public string file_name;
        public string file_mime;
        public string download_url;
        public string content;
    }
}
