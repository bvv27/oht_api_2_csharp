using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CancelProjectResult CancelProject(string projectId)
        {
            var r = new CancelProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    
                    string json = client.UploadString(web, "DELETE", "");
                    r = JsonConvert.DeserializeObject<CancelProjectResult>(json);
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
