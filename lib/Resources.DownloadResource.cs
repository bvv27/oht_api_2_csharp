using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace oht.lib
{
    partial class Ohtapi
    {
        public DownloadResourceResult DownloadResource(string public_key, string secret_key, string resource_uuid, int project_id = 0)
        {
            var r = new DownloadResourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/resources/" + resource_uuid + "/download?secret_key={0}&public_key={1}&project_id={2}", secret_key, public_key, project_id);
                    r.file = client.DownloadData(web);
                    r.status.Code = 0;
                    r.status.Msg = "ok";
                    
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }

        public DownloadResourceResult DownloadResource(string resource_uuid, int project_id = 0)
        {
            return DownloadResource(KeyPublic, KeySecret, resource_uuid, project_id);
        }
    }

    public struct DownloadResourceResult
    {

        public StatusType status;
        public byte[] file;
        
        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
}
