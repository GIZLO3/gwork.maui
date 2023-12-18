using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class OfferDetailsPage : ContentPage
{
	public OfferDetailsPage()
	{
		InitializeComponent();
		BindingContext = new OfferDetailsPageViewModel();
	}
}