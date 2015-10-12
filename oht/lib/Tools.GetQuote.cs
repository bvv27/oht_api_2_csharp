using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetQuoteResult GetQuote(string resources, string wordcount, string source_language, string target_language
            , string service="", string expertise="", string proofreading="", string currency="")
        {
            var r = new GetQuoteResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = System.Text.Encoding.UTF8;
                    var web = Url + String.Format("/tools/quote?public_key={0}&secret_key={1}&resources={2}&wordcount={3}&source_language={4}&target_language={5}&service={6}&expertise={7}&proofreading={8}&currency={9}"
                        , KeyPublic, KeySecret, resources, wordcount, source_language, target_language, service, expertise, proofreading, currency);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetQuoteResult>(json);
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

    public enum currency
    {
        USD,EUR
    }
    public struct GetQuoteResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType status;
        [JsonProperty(PropertyName = "results")]
        public GetQuoteResultType result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] results;
        [JsonProperty(PropertyName = "errors")]
        public string[] errors;

        public override string ToString()
        {
            return status.Code == 0 ? status.Msg : status.Code + " " + status.Msg;
        }
    }
    public struct GetQuoteResultType
    {
        [JsonProperty(PropertyName = "currency")]
        public string currency;
        [JsonProperty(PropertyName = "account_username")]
        public string account_username;
        [JsonProperty(PropertyName = "credits")]
        public decimal credits;
        [JsonProperty(PropertyName = "resources")]
        public GetQuoteResources[] resources;
        [JsonProperty(PropertyName = "total")]
        public GetQuoteTotal total;
    }

    public struct GetQuoteResources
    {
        [JsonProperty(PropertyName = "price")]
        public decimal price;
        [JsonProperty(PropertyName = "resource")]
        public string resource;
        [JsonProperty(PropertyName = "wordcount")]
        public int wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal credits;
    }
    public struct GetQuoteTotal
    {
        [JsonProperty(PropertyName = "net_price")]
        public decimal net_price;
        [JsonProperty(PropertyName = "transaction_fee")]
        public decimal transaction_fee;
        [JsonProperty(PropertyName = "price")]
        public decimal price;
        [JsonProperty(PropertyName = "wordcount")]
        public int wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal credits;
    }
}
