using gwork.maui.Controls;

namespace gwork.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            (navBar as NavBar)?.PageAppearing();
        }
    }
}
