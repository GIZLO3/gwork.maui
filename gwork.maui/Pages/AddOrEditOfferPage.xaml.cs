using gwork.maui.ViewModels;

namespace gwork.maui.Pages;

public partial class AddOrEditOfferPage : ContentPage
{
	public AddOrEditOfferPage()
	{
		InitializeComponent();
		BindingContext = new AddOrEditOfferPageViewModel();
	}
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue)) 
            return;

        if (!double.TryParse(e.NewTextValue, out double value))
        {
            ((Entry)sender).Text = e.OldTextValue;
        }
    }
}