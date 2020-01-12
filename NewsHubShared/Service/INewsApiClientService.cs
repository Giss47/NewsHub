using NewsAPI.Constants;
using NewsAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsHubShared.Service
{
    public interface INewsApiClientService
    {
        Task<ArticlesResult> GetTopHeadlines(int countryId);
    }
}