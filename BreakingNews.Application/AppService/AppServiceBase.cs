using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using Newtonsoft.Json;

namespace BreakingNews.Application.AppService
{
    public class AppServiceBase<TEntity> : IDisposable, IAppServiceBase<TEntity> where TEntity : class
    {
        protected readonly string BaseUrl;
        protected readonly string MethodUrl;

        public AppServiceBase(string baseUrl, string methodUrl)
        {
            BaseUrl = baseUrl;
            MethodUrl = $"api/{methodUrl}";
        }

        public async Task<TEntity> Add(TEntity obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage(HttpMethod.Post, MethodUrl)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };

                var result = await client.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                    return null;

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TEntity>(json);
            }
        }

        public async Task<TEntity> Update(int id, TEntity obj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage(HttpMethod.Put, $"{MethodUrl}/{id}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
                };

                var result = await client.SendAsync(request);

                if (!result.IsSuccessStatusCode)
                    return null;

                var json = await result.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<TEntity>(json);
            }
        }

        public async Task Remove(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                await client.DeleteAsync($"{MethodUrl}/{id}");
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync(MethodUrl);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<TEntity>>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public async Task<TEntity> GetById(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"{MethodUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TEntity>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public void Dispose()
        { }
    }
}