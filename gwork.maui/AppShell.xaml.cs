using gwork.maui.Pages;
using gwork.maui.Pages.AdminPages;

namespace gwork.maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LogInPage), typeof(LogInPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));
            Routing.RegisterRoute(nameof(OfferDetailsPage), typeof(OfferDetailsPage));
            
            Routing.RegisterRoute(nameof(AdminPanelPage), typeof(AdminPanelPage));
            Routing.RegisterRoute(nameof(AddOrEditOfferPage), typeof(AddOrEditOfferPage));
            Routing.RegisterRoute(nameof(OffersListPage), typeof(OffersListPage));
        }
    }
}
