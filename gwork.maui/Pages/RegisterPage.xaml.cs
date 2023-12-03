using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		this.BindingContext = new RegisterPageViewModel();
	}

    private void RegisterButtonClicked(object sender, EventArgs e)
    {

    }
}