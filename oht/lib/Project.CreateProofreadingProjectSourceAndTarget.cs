using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateProofreadingProjectSourceAndTargetResult CreateProofreadingProjectSourceAndTarget(string source_language
            ,string target_language , string sources, string translations, string wordcount="", string notes="", string callback_url="", string name="")
        {
            var r = new CreateProofreadingProjectSourceAndTargetResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/proof-translated?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&translations={5}&wordcount={6}&callback_url={7}&name={8}"
                        ,KeyPublic, KeySecret, source_language,target_language,  sources,translations, wordcount, callback_url, name);
                    var values = new System.Collections.Specialized.NameValueCollection();
                    values.Add("notes", notes);
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceAndTargetResult>(json);
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


    public struct CreateProofreadingProjectSourceAndTargetResult
    {

        public StatusType status;
        public CreateProofreadingProjectSourceAndTargetType[] results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceAndTargetType
    {
        public int project_id;
        public int wordcount;
        public decimal credits;
    }



}
