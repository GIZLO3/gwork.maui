using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		InitializeComponent();
		BindingContext = new LogInPageViewModel();
	}
}