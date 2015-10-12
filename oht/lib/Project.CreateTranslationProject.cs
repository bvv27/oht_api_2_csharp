using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateTranslationProjectResult CreateTranslationProject(string sourceLanguage, string targetLanguage
            ,string sources, string expertise, string wordcount="", string notes="", string callbackUrl="", string name="")
        {
            var r = new CreateTranslationProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/translation?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&expertise={5}"
                        , KeyPublic, KeySecret, sourceLanguage, targetLanguage, sources, expertise);


                    var values = new System.Collections.Specialized.NameValueCollection
                    {
                        {"wordcount", wordcount},
                        {"notes", notes},
                        {"callback_url", callbackUrl},
                        {"name", name}
                    };

                    string json = Encoding.Default.GetString(client.UploadValues(web,"POST", values));
                    r = JsonConvert.DeserializeObject<CreateTranslationProjectResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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


    public struct CreateTranslationProjectResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateTranslationProjectType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateTranslationProjectType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
