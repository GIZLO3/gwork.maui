using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage()
	{
		InitializeComponent();
		BindingContext = new UserDetailsPageViewModel();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		if(BindingContext is UserDetailsPageViewModel userDetailsPageViewModel)
		{
			userDetailsPageViewModel.GetEmployeeDetails();
			userDetailsPageViewModel.GetAppliedOffers();
		}
    }
}