using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = new RegisterPageViewModel();
	}
}