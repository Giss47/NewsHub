using System;
using System.Collections.Generic;
using System.Text;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public class NewsApiClientService : INewsApiClientService
    {
        private readonly string ApiKey = "fa8852f23b41418d90be8028fc382b66";

        public async Task<ArticlesResult> GetTopHeadlines(int countryId)
        {
            var newsApiClient = new NewsApiClient(ApiKey);
            ArticlesResult articlesResponse = await newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Country = (Countries)countryId
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                return articlesResponse;
            }

            return null;
        }
    }
}