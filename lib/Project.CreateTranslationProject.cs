using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public CreateTranslationProjectResult CreateTranslationProject(string source_language, string target_language
            ,string sources, string expertise, string wordcount="", string notes="", string callback_url="", string name="")
        {
            var r = new CreateTranslationProjectResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/projects/translation?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&expertise={5}"
                        , KeyPublic, KeySecret, source_language, target_language, sources, expertise);


                    var values = new System.Collections.Specialized.NameValueCollection();
                    values.Add("wordcount", wordcount);
                    values.Add("notes", notes);
                    values.Add("callback_url", callback_url);
                    values.Add("name", name);
 
                    string json = Encoding.Default.GetString(client.UploadValues(web,"POST", values));
                    r = JsonConvert.DeserializeObject<CreateTranslationProjectResult>(json);


                    //var web = Url + String.Format("/projects/translation?public_key={0}&secret_key={1}&source_language={2}&target_language={3}&sources={4}&wordcount={5}&notes={6}&expertise={7}&callback_url={8}&name={9}"
                    //    ,KeyPublic, KeySecret, source_language, target_language, sources, wordcount, notes, expertise, callback_url, name);
                    //string json = client.DownloadString(web);
                    //r = JsonConvert.DeserializeObject<CreateTranslationProjectResult>(json);
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


    public struct CreateTranslationProjectResult
    {

        public StatusType status;
        public CreateTranslationProjectType results;
        public string[] errors;

        public string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct CreateTranslationProjectType
    {
        public int project_id;
        public int wordcount;
        public decimal credits;
    }



}
