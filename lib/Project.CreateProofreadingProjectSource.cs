using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateProofreadingProjectSourceResult CreateProofreadingProjectSource(string source_language
            , string sources, string wordcount="", string notes="",string expertise ="", string callback_url="", string name="")
        {
            var r = new CreateProofreadingProjectSourceResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/proof-general?public_key={0}&secret_key={1}&source_language={2}&sources={3}&wordcount={4}&expertise={5}&callback_url={6}&name={7}"
                        ,KeyPublic, KeySecret, source_language, sources, wordcount, expertise, callback_url, name);
                    var values = new System.Collections.Specialized.NameValueCollection();
                    values.Add("notes", notes);
                    string json = Encoding.Default.GetString(client.UploadValues(web, "POST", values));
                    r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceResult>(json);
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


    public struct CreateProofreadingProjectSourceResult
    {

        public StatusType status;
        public CreateProofreadingProjectSourceType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code + " " + status.Msg;
        }
    }
    public struct CreateProofreadingProjectSourceType
    {
        public int project_id;
        public int wordcount;
        public decimal credits;
    }



}
