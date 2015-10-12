using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateProofreadingProjectSourceResult CreateProofreadingProjectSource(string sourceLanguage
            , string sources, string wordcount="", string notes="",string expertise ="", string callbackUrl="", string name="")
        {
            var r = new CreateProofreadingProjectSourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/proof-general?public_key={0}&secret_key={1}&source_language={2}&sources={3}&wordcount={4}&expertise={5}&callback_url={6}&name={7}"
                        ,KeyPublic, KeySecret, sourceLanguage, sources, wordcount, expertise, callbackUrl, name);
                    var values = new System.Collections.Specialized.NameValueCollection {{"notes", notes}};
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceResult>(json);
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


    public struct CreateProofreadingProjectSourceResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateProofreadingProjectSourceType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
