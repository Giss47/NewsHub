using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public class NewsApiClientService : INewsApiClientService
    {
        private readonly string ApiKey = "fa8852f23b41418d90be8028fc382b66";

        public async Task<ArticlesResult> GetNewsSearchResult(int languageId, int sortById, string q, DateTime fromDate)
        {
            var newsApiClient = new NewsApiClient(ApiKey);
            ArticlesResult articlesResponse = await newsApiClient.GetEverythingAsync(new EverythingRequest
            {
                Q = q,
                SortBy = (SortBys)sortById,
                Language = (Languages)languageId,
                From = fromDate
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                return articlesResponse;
            }

            return null;
        }

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