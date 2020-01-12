using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using NewsAPI.Models;
using NewsHubShared.Service;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace NewsHubShared.ViewModels
{
    public class NewsViewModel : ReactiveObject
    {
        public ReadOnlyObservableCollection<Article> Items;

        private SourceList<Article> articleSource;

        public ReactiveCommand<Unit, Task> LoadArticles { get; }

        readonly INewsApiClientService newsApiClientService;

        public NewsViewModel()
        {
            newsApiClientService = Startup.ServiceProvider.GetService<INewsApiClientService>();
            articleSource = new SourceList<Article>();
            articleSource.Connect()
                  .ObserveOn(RxApp.MainThreadScheduler)
                  .Bind(out Items)
                  .Subscribe();

            LoadArticles = ReactiveCommand.Create(async () =>
            {
                ArticlesResult articlesResult = await newsApiClientService.GetTopHeadlines(51);
                if (Items.Count != 0)
                {
                    articleSource.Clear();                   
                }
                foreach (var article in articlesResult.Articles)
                {
                    articleSource.Add(article);

                }
            });
        }
    }
}