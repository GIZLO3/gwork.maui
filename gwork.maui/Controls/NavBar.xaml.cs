using gwork.maui.Pages;

namespace gwork.maui.Controls;

public partial class NavBar : ContentView
{
	public NavBar()
	{
		InitializeComponent();
	}

    private async void GoToLogInPage(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync(nameof(LogInPage));
    }

    private async void GoToRegisterPage(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(RegisterPage));
    }
}