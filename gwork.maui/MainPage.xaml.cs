using gwork.maui.Controls;
using gwork.maui.ViewModels;

namespace gwork.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (navBar as NavBar)?.PageAppearing();
        }
    }
}
