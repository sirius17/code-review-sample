using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiRelay
{
    public class APIClient : IDisposable
    {
        private static List<string> skipHeader = new List<string> { "accept", "accept-encoding", "host", "connection", "content-length", "content-type", "host", "origin", "referer", "user-agent", "cookie", "cache-control", "x-postman-interceptor-id", "postman-token", "dnt", "ms-aspnetcore-token", "x-original-proto", "x-original-for", "query-path" };

        private HttpClient _httpClient { get; set; }

        private string _url { get; set; }

        public APIClient(string url)
        {
            _url = url;

            HttpClientHandler handler = new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip
            };

            _httpClient = new HttpClient(handler);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> GetAsync()
        {
            return await _httpClient.GetAsync(_url);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(T content)
        {
            return await _httpClient.PutAsync(_url, CreateHttpContent<T>(content));
        }

        public async Task<HttpResponseMessage> PostAsync<T>(T content)
        {
            return await _httpClient.PostAsync(_url, CreateHttpContent<T>(content));
        }

        public async Task<HttpResponseMessage> DeleteAsync()
        {
            return await _httpClient.GetAsync(_url);
        }

        private void AddHeader(Header header)
        {
            _httpClient.DefaultRequestHeaders.Remove(header.Key);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        }

        public void AddHeaders(List<Header> headers)
        {
            headers
                .FindAll(x => !skipHeader.Contains(x.Key.ToLower()))
                .ForEach(AddHeader);
        }

        public void Dispose()
        {
        }
    }
}
