using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using BreakingNews.Domain.Entities;
using Newtonsoft.Json;

namespace BreakingNews.Application.AppService
{
    public class NewsAppService : AppServiceBase<News>, INewsAppService
    {
        public NewsAppService() : base("https://localhost:44343/", "News")
        { }

        public async Task<News> GetByFriendlyName(string friendlyName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync($"{MethodUrl}/{friendlyName}");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<News>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public async Task<IEnumerable<News>> GetPublicNews()
        {
            var news = await GetAll();

            return news.Where(w => w.IsPublished);
        }
    }
}
