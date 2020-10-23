using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NLog;
namespace Money.Helper
{
    public class HttpHelper
    {
        Logger nlog = LogManager.GetCurrentClassLogger();
        int errorCount = 0;
        public T GetData<T>(string url, Dictionary<string, string> header = null, bool is950Encode = false)
        {
            return GetData<T>(new Uri(url), is950Encode: is950Encode);
        }
        public T GetData<T>(Uri url, Dictionary<string, string> header = null, bool is950Encode = false)
        {
            try
            {
                string responseData = HttpGet(url, header, is950Encode: is950Encode);
                var Tobject = JsonConvert.DeserializeObject<T>(responseData); ;
                errorCount = 0;
                return Tobject;
            }
            catch (JsonException ex)
            {
                if (errorCount == 10) { return default(T); }
                errorCount++;
                return GetData<T>(url, is950Encode: is950Encode);
            }
            catch (Exception ex)
            {
                if (errorCount == 10) { return default(T); }
                errorCount++;
                return GetData<T>(url, is950Encode: is950Encode);
            }
        }
        public string GetData(string url, Dictionary<string, string> header = null, bool is950Encode = false)
        {
            return GetData(new Uri(url), is950Encode: is950Encode);
        }
        public string GetData(Uri url, Dictionary<string, string> header = null, bool is950Encode = false)
        {
            try
            {
                string responseData = HttpGet(url, header , is950Encode: is950Encode);
                errorCount = 0;
                return responseData;
            }
            catch (Exception ex)
            {
                if (errorCount == 10) { return ""; }
                errorCount++;
                return GetData(url, is950Encode: is950Encode);
            }
        }

        private string HttpGet(Uri uri, Dictionary<string, string> header = null, bool is950Encode = false)
        {
            string responseData = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponseAsync().Result;
                Stream stream = response.GetResponseStream();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                StreamReader reader = new StreamReader(stream, is950Encode ? Encoding.GetEncoding(950) : Encoding.UTF8);
                responseData = reader.ReadToEnd();
            }
            catch (AggregateException ex)
            {
                nlog.Error(ex, $"{DateTime.Now }  error  get error {uri} ");
                Console.WriteLine(ex.Message);
            }
            return responseData;
        }
        private HttpClient SetHttpHeader(HttpClient httpClient, Dictionary<string, string> header = null)
        {
            if (header != null)
            {
                foreach (var d in header.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(d, header[d]);
                }
            }

            return httpClient;
        }
    }
}
