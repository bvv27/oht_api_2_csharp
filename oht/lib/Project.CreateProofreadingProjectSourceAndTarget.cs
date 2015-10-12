using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateProofreadingProjectSourceAndTargetResult CreateProofreadingProjectSourceAndTarget(string sourceLanguage
            ,string targetLanguage , string sources, string translations, string wordcount="", string notes="", string callbackUrl="", string name="")
        {
            var r = new CreateProofreadingProjectSourceAndTargetResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/projects/proof-translated?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&translations={5}&wordcount={6}&callback_url={7}&name={8}"
                        ,KeyPublic, KeySecret, sourceLanguage,targetLanguage,  sources,translations, wordcount, callbackUrl, name);
                    var values = new System.Collections.Specialized.NameValueCollection {{"notes", notes}};
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceAndTargetResult>(json);
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


    public struct CreateProofreadingProjectSourceAndTargetResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public CreateProofreadingProjectSourceAndTargetType[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code + " " + Status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceAndTargetType
    {
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }



}
