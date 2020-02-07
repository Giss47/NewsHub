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
    public class AdvancedSearchViewModel : ReactiveObject
    {
        private string q;
        public string Q
        {
            get => q;
            set => this.RaiseAndSetIfChanged(ref q, value);
        }

        private int sortbyIndex;
        public int SortbyIndex
        {
            get => sortbyIndex;
            set => this.RaiseAndSetIfChanged(ref sortbyIndex, value);
        }

        private int languageIndex = 14;
        public int LanguageIndex
        {
            get => languageIndex;
            set => this.RaiseAndSetIfChanged(ref languageIndex, value);
        }

        // Due to API limitation, the date is already hard coded
        public DateTime FromDate { get; }

        public ReactiveCommand<Unit, Task> SearchArticles { get; }

        public ReadOnlyObservableCollection<Article> Items;

        private readonly SourceList<Article> articleSource;

        public List<string> LanguagiesList;
        public List<string> SortByList;

        readonly INewsApiClientService newsApiClientService;
        public AdvancedSearchViewModel()
        {
            newsApiClientService = Startup.ServiceProvider.GetService<INewsApiClientService>();
            FromDate = DateTime.Today.AddMonths(-1);
            articleSource = new SourceList<Article>();
            LanguagiesList = Enum.GetNames(typeof(Languages)).Cast<string>().ToList();
            SortByList = Enum.GetNames(typeof(SortBys)).Cast<string>().ToList();
            articleSource.Connect()
                  .ObserveOn(RxApp.MainThreadScheduler)
                  .Bind(out Items)
                  .Subscribe();

            SearchArticles = ReactiveCommand.Create(async () =>
            {
                ArticlesResult articlesResult = await newsApiClientService.GetNewsSearchResult(LanguageIndex, SortbyIndex, Q, FromDate);
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