using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CancelProjectResult CancelProject(string project_id)
        {
            var r = new CancelProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + project_id + "?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    
                    string json = client.UploadString(web, "DELETE", "");
                    r = JsonConvert.DeserializeObject<CancelProjectResult>(json);
                }
            }
            catch (Exception err)
            {
                r.status.Code = -1;
                r.status.Msg = err.Message;
            }
            return r;
        }


    }


    public struct CancelProjectResult
    {

        public StatusType status;
        public string[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }

}
