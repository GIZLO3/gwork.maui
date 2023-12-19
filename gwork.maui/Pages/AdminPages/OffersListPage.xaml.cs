using gwork.maui.ViewModels;

namespace gwork.maui.Pages.AdminPages;

public partial class OffersListPage : ContentPage
{
	public OffersListPage()
	{
		InitializeComponent();

		BindingContext = new OffersListPageViewModel();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is OffersListPageViewModel offersListPageViewModel)
            offersListPageViewModel.GetOffers();
    }
}