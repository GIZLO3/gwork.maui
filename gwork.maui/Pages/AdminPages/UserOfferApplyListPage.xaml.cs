using gwork.maui.ViewModels;

namespace gwork.maui.Pages.AdminPages;

public partial class UserOfferApplyListPage : ContentPage
{
	public UserOfferApplyListPage()
	{
		InitializeComponent();
		BindingContext = new UserOfferApplyListPageViewModel();
	}
}