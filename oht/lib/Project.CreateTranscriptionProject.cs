using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateTranscriptionProjectResult CreateTranscriptionProject(string sourceLanguage
            , string sources, string length = "", string notes = "", string callbackUrl = "", string name = "")
        {
            var r = new CreateTranscriptionProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/transcription?public_key={0}&secret_key={1}&source_language={2}&sources={3}&length={4}"
                        , KeyPublic, KeySecret, sourceLanguage, sources, length);
                    var values = new System.Collections.Specialized.NameValueCollection
                    {
                        {"notes", notes},
                        {"callback_url", callbackUrl},
                        {"name", name}
                    };


                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateTranscriptionProjectResult>(json);
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


    public struct CreateTranscriptionProjectResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateTranscriptionProjectType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateTranscriptionProjectType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "length")]
        public int Length;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
