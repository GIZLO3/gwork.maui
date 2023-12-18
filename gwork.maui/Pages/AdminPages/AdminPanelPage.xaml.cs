using gwork.maui.ViewModels;

namespace gwork.maui.Pages.AdminPages;

public partial class AdminPanelPage : ContentPage
{
	public AdminPanelPage()
	{
		InitializeComponent();
		BindingContext = new AdminPanelPageViewModel();
	}
}