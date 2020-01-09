using System;
using System.Collections.Generic;
using System.Text;
using NewsAPI;
using NewsAPI.Models;
using NewsAPI.Constants;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public class NewsApiClientService
    {
        private readonly string ApiKey = "fa8852f23b41418d90be8028fc382b66";

        public ArticlesResult GetTopHeadlines(Countries country)
        {
            var newsApiClient = new NewsApiClient(ApiKey);
            var articlesResponse = newsApiClient.GetTopHeadlinesAsync(new TopHeadlinesRequest
            {
                Country = country
            });

            if (articlesResponse.Result.Status == Statuses.Ok)
            {
                return articlesResponse.Result;
            }

            return null;
        }
    }
}