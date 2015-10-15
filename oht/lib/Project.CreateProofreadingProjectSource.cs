using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface ICreateProofreadingProjectSourceProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string sources
            , string wordcount = "", string notes = "", string expertise = "", string callbackUrl = "", string name = "", string[] custom = null);
    }
    public class CreateProofreadingProjectSourceProvider : ICreateProofreadingProjectSourceProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string sourceLanguage, string sources
            , string wordcount, string notes, string expertise, string callbackUrl, string name, string[] custom = null)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/proof-general?public_key={0}&secret_key={1}", publicKey, secretKey);
                var values = new System.Collections.Specialized.NameValueCollection
                {
                    {"source_language", sourceLanguage }
                    ,{"sources", sources }
                    ,{"wordcount", wordcount }
                    ,{"expertise", expertise }
                    ,{"callback_url", callbackUrl }
                    ,{ "notes", notes },{ "name", name }
                };
                if (custom != null)
                {
                    for (var i = 0; i < custom.Length; i++)
                    {
                        values.Add("custom" + i, custom[i]);
                    }
                }
                return Encoding.Default.GetString(client.UploadValues(web, "POST", values));
            }
        }
    }
    //http://sandbox.onehourtranslation.com/api/2/projects/proof-general?notes=&sources=rsc-5618b9e3e272e2-73253523&secret_key=35aec76f5d9a015304173d1d81891f65&expertise=marketing-consumer-media&name=unittest+proof_translated+2015-10-13+17%3A23-02&source_language=en-us&callbackUrl=&public_key=c7t9NbMpG2xK6nvD834B&wordCount=0

    partial class Ohtapi
    {
        public ICreateProofreadingProjectSourceProvider CreateProofreadingProjectSourceProvider;
        public CreateProofreadingProjectSourceResult CreateProofreadingProjectSource(string sourceLanguage
            , string sources, string wordcount, string notes,string expertise, string callbackUrl, string name)
        {
            var r = new CreateProofreadingProjectSourceResult();
            try
            {
                if (CreateProofreadingProjectSourceProvider == null)
                    CreateProofreadingProjectSourceProvider = new CreateProofreadingProjectSourceProvider();
                var json = CreateProofreadingProjectSourceProvider.Get(Url, _proxy, KeyPublic, KeySecret, sourceLanguage, sources, wordcount, notes, expertise, callbackUrl, name);
                r = JsonConvert.DeserializeObject<CreateProofreadingProjectSourceResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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
