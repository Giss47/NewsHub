using NewsAPI.Models;
using System;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public interface INewsApiClientService
    {
        Task<ArticlesResult> GetTopHeadlines(int countryId);
        Task<ArticlesResult> GetNewsSearchResult(int languageId, int sortByID, string q, DateTime fromDate);
    }
}