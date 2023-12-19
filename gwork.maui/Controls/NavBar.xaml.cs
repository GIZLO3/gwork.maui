using gwork.maui.Pages;
using gwork.maui.Pages.AdminPages;

namespace gwork.maui.Controls;

public partial class NavBar : ContentView
{
    private Button? adminPanelButton;

    public NavBar()
	{
		InitializeComponent();
	}

    public void PageAppearing()
    {
        if (App.LoggedUser != null)
        {
            navBarButton1.Text = "Profil";
            navBarButton2.Text = "Wyloguj siê";

            if(App.LoggedUser.IsAdmin && adminPanelButton == null)
            {
                adminPanelButton = new Button();
                adminPanelButton.Text = "Panel admina";
                adminPanelButton.Clicked += NavBarAdminPanelButtonClicked;

                mainVerticalStackLayout.Children.Add(adminPanelButton);
            }
        }
        else
        {
            navBarButton1.Text = "Zaloguj siê";
            navBarButton2.Text = "Zarejestuj siê";
        }
    }

    private void NavBarButton1Clicked(object sender, EventArgs e)
    {
        if (App.LoggedUser != null)
        {
            Shell.Current.GoToAsync(nameof(UserDetailsPage));
        }
        else//otwarcie strony logowania
        {
            Shell.Current.GoToAsync(nameof(LogInPage));
        }
    }

    private void NavBarButton2Clicked(object sender, EventArgs e)
    {
        if (App.LoggedUser != null)//obs³uga wylogowania siê
        {
            if (App.LoggedUser.IsAdmin && adminPanelButton != null)
            {
                mainVerticalStackLayout.Children.Remove(adminPanelButton);
                adminPanelButton = null;
            }

            App.LoggedUser = null;
            File.Delete(App.LoggedUserJsonFilePath);
            PageAppearing();
        }
        else//otwarcie strony rejestracji
        {
            Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }

    private void NavBarAdminPanelButtonClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AdminPanelPage));
    }
}