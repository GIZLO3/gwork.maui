using gwork.maui.Models;
using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class UserDetailsPage : ContentPage
{
	public UserDetailsPage()
	{
		InitializeComponent();
		BindingContext = new UserDetailsPageViewModel();
	}
}