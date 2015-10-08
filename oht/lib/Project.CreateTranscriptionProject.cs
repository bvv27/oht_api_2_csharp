using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateTranscriptionProjectResult CreateTranscriptionProject(string source_language
            , string sources, string length = "", string notes = "", string callback_url = "", string name = "")
        {
            var r = new CreateTranscriptionProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/transcription?public_key={0}&secret_key={1}&source_language={2}&sources={3}&length={4}"
                        , KeyPublic, KeySecret, source_language, sources, length);
                    var values = new System.Collections.Specialized.NameValueCollection();
                    
                    values.Add("notes", notes);
                    values.Add("callback_url", callback_url);
                    values.Add("name", name);

                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateTranscriptionProjectResult>(json);
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


    public struct CreateTranscriptionProjectResult
    {

        public StatusType status;
        public CreateTranscriptionProjectType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
    public struct CreateTranscriptionProjectType
    {
        public int project_id;
        public int length;
        public decimal credits;
    }



}
