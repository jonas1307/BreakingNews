using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BreakingNews.Application.Interfaces;
using BreakingNews.Domain.Entities;
using BreakingNews.Domain.Interfaces.Services;
using Newtonsoft.Json;

namespace BreakingNews.Application.AppService
{
    public class NewsAppService : AppServiceBase<News>, INewsAppService
    {
        private readonly INewsService _newsService;
        private const string ApiUrl = "https://localhost:44343/";

        public NewsAppService(IServiceBase<News> serviceBase, INewsService newsService) : base(serviceBase)
        {
            _newsService = newsService;
        }

        public async Task<News> GetByFriendlyName(string friendlyName)
        {
            return await _newsService.QuerySingle(qry => qry.FriendlyUrl == friendlyName);
        }

        public async Task<IEnumerable<News>> GetNewsByWebService()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/news");

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<News>>(await response.Content.ReadAsStringAsync());
                }
            }

            return null;
        }

        public async Task<IEnumerable<News>> GetPublicNews()
        {
            return await _newsService.QueryMultiple(qry => qry.IsPublished);
        }
    }
}
