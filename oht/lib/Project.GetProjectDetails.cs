using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    public interface IGetProjectDetailsProvider
    {
        string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId);
    }
    public class GetProjectDetailsProvider : IGetProjectDetailsProvider
    {
        public string Get(string url, WebProxy proxy, string publicKey, string secretKey, string projectId)
        {
            using (var client = new WebClient())
            {
                if (proxy != null)
                    client.Proxy = proxy;
                client.Encoding = Encoding.UTF8;
                var web = url + String.Format("/projects/" + projectId + "?public_key={0}&secret_key={1}"
                    , publicKey, secretKey);
                return client.DownloadString(web);
            }
        }
    }

    partial class Ohtapi
    {
        public IGetProjectDetailsProvider GetProjectDetailsProvider;
        /// <summary>
        /// Receive project specifications 
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public GetProjectDetailsResult GetProjectDetails(string projectId)
        {
            var r = new GetProjectDetailsResult();
            // ReSharper disable once CollectionNeverQueried.Local
            List<string[]> resourceBinding = new List<string[]>();
            try
            {
                if (GetProjectDetailsProvider == null)
                    GetProjectDetailsProvider = new GetProjectDetailsProvider();
                var json = GetProjectDetailsProvider.Get(Url, _proxy, KeyPublic, KeySecret, projectId).Replace("\"results\":[", "\"resultsArray\":[");
                var n = json.IndexOf("\"resource_binding\":{\"", StringComparison.Ordinal);

                if (n > -1)
                {
                    var nn = json.IndexOf("}", n, StringComparison.Ordinal);
                    var str = json.Substring(n, nn - n + 2);
                    json = json.Replace(str, "");
                    var ss = str.Substring(19, str.Length - 2-19).Split(',');
                    resourceBinding.AddRange(from s in ss
                        select s.Split(':')
                        into item
                        where item.Length == 2
                        select new[]
                        {
                            item[0].Substring(1, item[0].Length - 2), item[1].Substring(2, item[1].Length - 4)
                        });
                }
                r = JsonConvert.DeserializeObject<GetProjectDetailsResult>(json);
                if (r.Status.Code == 0)
                {
                    r.Result.ResourceBinding = resourceBinding;
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
        [JsonProperty(PropertyName = "linguist_uuid")]
        public string LinguistUuid;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetProjectDetailsType
    {
        /// <summary>
        /// The unique id of the requested project
        /// </summary>
        [JsonProperty(PropertyName = "project_id")]
        public int ProjectId;
        /// <summary>
        /// Translation | Expert Translation | Proofreading | Transcription | Translation + Proofreading |
        /// </summary>
        [JsonProperty(PropertyName = "project_type")]
        public string ProjectType;
        [JsonProperty(PropertyName = "project_status")]
        public string ProjectStatus;
        /// <summary>
        /// Pending | in_progress | submitted | signed | completed | canceled
        /// pending - project submitted to OHT, but professional worker (translator/proofreader) did not start working yet
        /// in_progress - worker started working on this project
        /// submitted - the worker uploaded the first target resource to the project. This does not mean that the project is completed.
        /// signed - the worker declared (with his signature) that he finished working on this project and all resources have been uploaded.
        /// completed - final state of the project, after which we cannot guarantee fixes or corrections. This state is automatically enforced after 4 days of inactivity on the project.
        /// </summary>
        [JsonProperty(PropertyName = "project_status_code")]
        public string ProjectStatusCode;
        /// <summary>
        /// See Language Codes
        /// </summary>
        [JsonProperty(PropertyName = "source_language")]
        public string SourceLanguage;
        /// <summary>
        /// See Language Code
        /// </summary>
        [JsonProperty(PropertyName = "target_language")]
        public string TargetLanguage;
        /// <summary>
        /// Resource UUID lists of the project's sources, translations, proofs and transcriptions
        /// </summary>
        [JsonProperty(PropertyName = "resources")]
        public GetProjectDetailsResources Resources;
        /// <summary>
        /// length - in seconds (transcription projects only);
        /// </summary>
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        /// <summary>
        /// length - in seconds (transcription projects only);
        /// </summary>
        [JsonProperty(PropertyName = "length")]
        public int Length;
        [JsonProperty(PropertyName = "custom")]
        public string Custom;
        [JsonProperty(PropertyName = "resource_binding")]
        public List<string[]> ResourceBinding;
    }

    public struct GetProjectDetailsResources
    {
        /// <summary>
        /// List of source resource UUIDs related to the requested project
        /// </summary>
        [JsonProperty(PropertyName = "sources")]
        public string[] Sources;
        /// <summary>
        /// List of translation resource UUIDs related to the requested project
        /// </summary>
        [JsonProperty(PropertyName = "translations")]
        public string[] Translations;
        /// <summary>
        /// List of proofreading resource UUIDs related to the requested project
        /// </summary>
        [JsonProperty(PropertyName = "proofs")]
        public string Proofs;
        /// <summary>
        /// List of transcription resource UUIDs related to the requested project
        /// </summary>
        [JsonProperty(PropertyName = "transcriptions")]
        public string Transcriptions;

    }

    public struct GetProjectDetailsCustom
    {
        [JsonProperty(PropertyName = "api_custom_0")]
        public string Custom0;
        [JsonProperty(PropertyName = "api_custom_1")]
        public string Custom1;
        [JsonProperty(PropertyName = "api_custom_2")]
        public string Custom2;
        [JsonProperty(PropertyName = "api_custom_3")]
        public string Custom3;
        [JsonProperty(PropertyName = "api_custom_4")]
        public string Custom4;
        [JsonProperty(PropertyName = "api_custom_5")]
        public string Custom5;
        [JsonProperty(PropertyName = "api_custom_6")]
        public string Custom6;
        [JsonProperty(PropertyName = "api_custom_7")]
        public string Custom7;
        [JsonProperty(PropertyName = "api_custom_8")]
        public string Custom8;
        [JsonProperty(PropertyName = "api_custom_9")]
        public string Custom9;
    }


}
