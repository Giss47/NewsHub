using DynamicData;
using Microsoft.Extensions.DependencyInjection;
using NewsAPI.Constants;
using NewsAPI.Models;
using NewsHubShared.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace NewsHubShared.ViewModels
{
    public class NewsViewModel : ReactiveObject
    {
        public ReadOnlyObservableCollection<Article> Items;

        public List<string> countriesList;

        private readonly SourceList<Article> articleSource;

        private int countryIndex;
        public int CountryIndex
        {
            get => countryIndex;
            set => this.RaiseAndSetIfChanged(ref countryIndex, value);
        }
        public ReactiveCommand<Unit, Task> LoadArticles { get; }

        readonly INewsApiClientService newsApiClientService;

        public NewsViewModel()
        {
            newsApiClientService = Startup.ServiceProvider.GetService<INewsApiClientService>();
            articleSource = new SourceList<Article>();
            countriesList = Enum.GetNames(typeof(Countries)).Cast<string>().ToList();
            articleSource.Connect()
                  .ObserveOn(RxApp.MainThreadScheduler)
                  .Bind(out Items)
                  .Subscribe();

            LoadArticles = ReactiveCommand.Create(async () =>
            {
                ArticlesResult articlesResult = await newsApiClientService.GetTopHeadlines(countryIndex);
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