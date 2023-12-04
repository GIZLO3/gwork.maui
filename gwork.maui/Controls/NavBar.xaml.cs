using gwork.maui.Pages;

namespace gwork.maui.Controls;

public partial class NavBar : ContentView
{
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
            App.LoggedUser = null;
            PageAppearing();
        }
        else//otwarcie strony rejestracji
        {
            Shell.Current.GoToAsync(nameof(RegisterPage));
        }
    }
}