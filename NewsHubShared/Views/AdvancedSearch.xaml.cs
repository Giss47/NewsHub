using Microsoft.Extensions.DependencyInjection;
using NewsHubShared.ViewModels;
using ReactiveUI;

namespace NewsHubShared.Views
{
    public class AdvancedSearchBase : ReactivePage<AdvancedSearchViewModel> { }
    public sealed partial class AdvancedSearch : AdvancedSearchBase
    {
        public AdvancedSearch()
        {
            this.InitializeComponent();
            ViewModel = Startup.ServiceProvider.GetService<AdvancedSearchViewModel>();
        }
    }
}