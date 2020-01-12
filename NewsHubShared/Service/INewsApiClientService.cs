using NewsAPI.Models;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public interface INewsApiClientService
    {
        Task<ArticlesResult> GetTopHeadlines(int countryId);
    }
}