using System;
using System.Text;
using Newtonsoft.Json;

namespace oht.lib
{
    partial class Ohtapi
    {
        public GetQuoteResult GetQuote(string resources, string wordcount, string sourceLanguage, string targetLanguage
            , string service="", string expertise="", string proofreading="", string currency="")
        {
            var r = new GetQuoteResult();
            try
            {
                using (var client = new System.Net.WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    var web = Url + String.Format("/tools/quote?public_key={0}&secret_key={1}&resources={2}&wordcount={3}&source_language={4}&target_language={5}&service={6}&expertise={7}&proofreading={8}&currency={9}"
                        , KeyPublic, KeySecret, resources, wordcount, sourceLanguage, targetLanguage, service, expertise, proofreading, currency);
                    string json = client.DownloadString(web);
                    r = JsonConvert.DeserializeObject<GetQuoteResult>(json.Replace("\"results\":[", "\"resultsArray\":["));
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

    public struct GetQuoteResult
    {
        [JsonProperty(PropertyName = "status")]
        public StatusType Status;
        [JsonProperty(PropertyName = "results")]
        public GetQuoteResultType Result;
        [JsonProperty(PropertyName = "resultsArray")]
        public string[] Results;
        [JsonProperty(PropertyName = "errors")]
        public string[] Errors;

        public override string ToString()
        {
            return Status.Code == 0 ? Status.Msg : Status.Code + " " + Status.Msg;
        }
    }
    public struct GetQuoteResultType
    {
        [JsonProperty(PropertyName = "currency")]
        public string Currency;
        [JsonProperty(PropertyName = "account_username")]
        public string AccountUsername;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
        [JsonProperty(PropertyName = "resources")]
        public GetQuoteResources[] Resources;
        [JsonProperty(PropertyName = "total")]
        public GetQuoteTotal Total;
    }

    public struct GetQuoteResources
    {
        [JsonProperty(PropertyName = "price")]
        public decimal Price;
        [JsonProperty(PropertyName = "resource")]
        public string Resource;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }
    public struct GetQuoteTotal
    {
        [JsonProperty(PropertyName = "net_price")]
        public decimal NetPrice;
        [JsonProperty(PropertyName = "transaction_fee")]
        public decimal TransactionFee;
        [JsonProperty(PropertyName = "price")]
        public decimal Price;
        [JsonProperty(PropertyName = "wordcount")]
        public int Wordcount;
        [JsonProperty(PropertyName = "credits")]
        public decimal Credits;
    }
}
