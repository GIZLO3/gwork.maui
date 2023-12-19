using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Pages;
using gwork.maui.Pages.AdminPages;

namespace gwork.maui.ViewModels
{
    public partial class AdminPanelPageViewModel : ObservableObject
    {
        [RelayCommand]
        private void GoToAddOfferPage()
        {
            Shell.Current.GoToAsync(nameof(AddOrEditOfferPage));
        }

        [RelayCommand]
        private void GoToAddOffersListPage()
        {
            Shell.Current.GoToAsync(nameof(OffersListPage));
        }

        [RelayCommand]
        private void GoToUserOfferApplyListPage()
        {
            Shell.Current.GoToAsync(nameof(UserOfferApplyListPage));
        }
    }
}