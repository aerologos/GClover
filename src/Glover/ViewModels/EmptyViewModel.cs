using Prism.Navigation.Regions;

namespace Glover.ViewModels;

public class EmptyViewModel : ViewModelBase, INavigationAware
{
    public bool IsNavigationTarget(NavigationContext navigationContext) => true;

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
