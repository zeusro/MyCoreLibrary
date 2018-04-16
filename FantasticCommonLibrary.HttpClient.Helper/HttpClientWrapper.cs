using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FantasticCommonLibrary.HttpClient.Helper;

namespace FantasticCommonLibrary.HttpClient.Helper
{
    public static class HttpClientWrapper
    {

        public async static Task<string> GetResponseTextAsync(this string url, string userAgent = null)
        {
            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return await url.GetAsync(userAgent);
        }

        public async static Task<string> GetResponseTextAsync(this string url, TimeSpan limit, string userAgent = null)
        {
            var client = new System.Net.Http.HttpClient();
            client.Timeout = limit;
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return await url.GetAsync(userAgent);
        }

        /// <summary>
        /// 异步获取post实体
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">post内容,这里直接用基类</param>
        /// <param name="userAgent">UA</param>
        /// <returns></returns>
        public static async Task<string> PostResponTextAsync(this string url, HttpContent content, string userAgent = null)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(userAgent))
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                return await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// todo:字典不允许重复的键,要改
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static async Task<string> PostFormResponTextAsync(this string url, Dictionary<string, string> formData,  string userAgent = null)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(userAgent))
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                return await (await client.PostAsync(url, new FormUrlEncodedContent(formData))).Content.ReadAsStringAsync();
            }
        }


        public static async Task<string> PostFormResponTextAsync(this string url, Dictionary<string, string> formData,  TimeSpan timeLimit,string userAgent = null)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.Timeout = timeLimit;
                if (!string.IsNullOrWhiteSpace(userAgent))
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                return await (await client.PostAsync(url, new FormUrlEncodedContent(formData))).Content.ReadAsStringAsync();
            }
        }


        /// <summary>
        /// 异步获取post实体
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">post内容,这里直接用基类</param>
        /// <param name="userAgent">UA</param>
        /// <returns></returns>
        public static async Task<string> PostResponTextAsync(this string url, HttpContent content, TimeSpan timeLimit, string userAgent = null)
        {

            var client = new System.Net.Http.HttpClient();
            client.Timeout = timeLimit;
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            return await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
        }
    }

    public static class HttpClientWrapper<T>
    {
      
        public static async Task<T> GetJsonEntityAsync(string url, string userAgent = null)
        {
            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            string response = await url.GetAsync(userAgent);
            return response.DeserializeObject<T>();
        }

        /// <summary>
        ///获取json实体,超时就见鬼了
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeLimit">时间限制</param>
        /// <param name="userAgent">UA</param>
        /// <returns>超时的话返回default T</returns>
        public static async Task<T> GetJsonEntityAsync(string url, TimeSpan timeLimit, string userAgent = null)
        {

            try
            {

                using (var client = new System.Net.Http.HttpClient())
                {
                    //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
                    client.Timeout = timeLimit;
                    if (!string.IsNullOrWhiteSpace(userAgent))
                        client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                    string response = await url.GetAsync(userAgent);
                    return response.DeserializeObject<T>();
                }

            }
            catch (TaskCanceledException)
            {

                return default(T);
            }
        }

        /// <summary>
        /// 反序列化实体有问题
        /// </summary>
        /// <param name="url"></param>
        /// <param name="formData"></param>
        /// <param name="userAgent"></param>
        /// <returns></returns>
        public static async Task<T> PostFormResponTextAsync(string url, Dictionary<string, string> formData, string userAgent = null)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                if (!string.IsNullOrWhiteSpace(userAgent))
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                string responseText = await (await client.PostAsync(url, new FormUrlEncodedContent(formData))).Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseText))
                    return responseText.DeserializeObject<T>(responseText);
                return default(T);
            }
        }

        public static async Task<T> PostFormResponTextAsync( string url, Dictionary<string, string> formData, TimeSpan timeLimit, string userAgent = null)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                client.Timeout = timeLimit;
                if (!string.IsNullOrWhiteSpace(userAgent))
                    client.DefaultRequestHeaders.Add("User-Agent", userAgent);
                string responseText = await (await client.PostAsync(url, new FormUrlEncodedContent(formData))).Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(responseText))
                    return responseText.DeserializeObject<T>(responseText);
                return default(T);
            }
        }


        /// <summary>
        /// 异步获取post实体
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">post内容,这里直接用基类</param>
        /// <param name="userAgent">UA</param>
        /// <returns></returns>
        public static async Task<T> GetPostResponJsonAsync(string url, HttpContent content, string userAgent = null)
        {

            var client = new System.Net.Http.HttpClient();
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            string response = await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
            return response.DeserializeObject<T>();
        }


        /// <summary>
        /// 异步获取post实体
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="content">post内容,这里直接用基类</param>
        /// <param name="userAgent">UA</param>
        /// <returns></returns>
        public static async Task<T> GetPostResponJsonAsync(string url, HttpContent content, TimeSpan timeLimit, string userAgent = null)
        {

            var client = new System.Net.Http.HttpClient();
            client.Timeout = timeLimit;
            //http://www.cnblogs.com/dudu/archive/2013/03/05/httpclient.html
            if (!string.IsNullOrWhiteSpace(userAgent))
                client.DefaultRequestHeaders.Add("User-Agent", userAgent);
            string response = await (await client.PostAsync(url, content)).Content.ReadAsStringAsync();
            return response.DeserializeObject<T>();
        }
    }
}
