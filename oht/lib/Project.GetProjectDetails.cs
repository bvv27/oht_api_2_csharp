using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetProjectDetailsResult GetProjectDetails(string projectId)
        {
            var r = new GetProjectDetailsResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/" + projectId + "?public_key={0}&secret_key={1}"
                        ,KeyPublic, KeySecret);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetProjectDetailsResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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


    public struct GetProjectDetailsResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetProjectDetailsType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetProjectDetailsType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "project_type")]
        public string ProjectType;
        [JsonProperty(PropertyName = "project_status")]
        public string ProjectStatus;
        [JsonProperty(PropertyName = "project_status_code")]
        public string ProjectStatusCode;
        [JsonProperty(PropertyName = "source_language")]
        public string SourceLanguage;
        [JsonProperty(PropertyName = "target_language")]
        public string TargetLanguage;
        [JsonProperty(PropertyName = "resources")]
        public GetProjectDetailsResources Resources;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "length")]
        public int Length;
        [JsonProperty(PropertyName = "custom")]
        public string Custom;
    }

    public struct GetProjectDetailsResources
    {
        [JsonProperty(PropertyName = "sources")]
        public string[] Sources;
        [JsonProperty(PropertyName = "translations")]
        public string[] Translations;
        [JsonProperty(PropertyName = "proofs")]
        public string Proofs;
        [JsonProperty(PropertyName = "transcriptions")]
        public string Transcriptions;

    }



}
