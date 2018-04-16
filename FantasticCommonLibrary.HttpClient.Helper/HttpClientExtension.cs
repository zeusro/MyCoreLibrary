using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FantasticCommonLibrary.HttpClient.Helper
{
    public static class HttpClientExtension
    {


        //public static async Task<T> PostAsync(this string url, HttpContent content, string ua = null)
        //{

        //    var client = new System.Net.Http.HttpClient();
        //    //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
        //    if (!string.IsNullOrWhiteSpace(ua))
        //        client.DefaultRequestHeaders.Add("User-Agent", ua);
        //    string response = await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
        //    return response;
        //}


        /// <summary>
        /// 异步获取
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static async Task<string> GetAsync(this string url, string userAgent = null)
        {
            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            string response = await (await client.GetAsync(url)).Content.ReadAsStringAsync();
            return response;
        }


        public static async Task<string> PostAsync(this string url, HttpContent content, string userAgent = null)
        {

            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            string response = await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
            return response;
        }

        public static async Task PostAsyncInDebug(this string url, HttpContent content, string userAgent = null)
        {
            System.Console.WriteLine(url);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            await client.PostAsync(url, content).ContinueWith(async t =>
            {
                string result = await t.Result.Content.ReadAsStringAsync();
                System.Console.WriteLine(JToken.Parse(result).ToString());
            });
            stopwatch.Stop();
            System.Console.WriteLine("运行了{0}毫秒", stopwatch.ElapsedMilliseconds);
        }


    }
}
