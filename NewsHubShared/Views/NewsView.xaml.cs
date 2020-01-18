using Microsoft.Extensions.DependencyInjection;
using NewsHubShared.ViewModels;
using ReactiveUI;

namespace NewsHubShared.Views
{
    public class NewsViewBase : ReactivePage<NewsViewModel> { }
    public sealed partial class NewsView : NewsViewBase
    {
        public NewsView()
        {
            this.InitializeComponent();
            ViewModel = Startup.ServiceProvider.GetService<NewsViewModel>();
        }
    }
}