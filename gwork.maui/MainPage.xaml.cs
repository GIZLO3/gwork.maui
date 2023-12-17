using gwork.maui.Controls;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.ViewModels;
using System.Text.Json;

namespace gwork.maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (File.Exists(App.LoggedUserJsonFilePath))
            {
                StreamReader streamReader = new(App.LoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                streamReader.Close();

                if (localUser != null)
                {
                    var userDatabase = new UserDatabase();
                    var storedUser = await userDatabase.GetUserAsync(localUser.Email);//pobranie z bazy uzytkownika 

                    if (storedUser != null)
                    {

                        if (localUser.Password == storedUser.Password)//sprawdzenie czy hasła się zgadzają
                            App.LoggedUser = storedUser;
                        else
                            File.Delete(App.LoggedUserJsonFilePath);
                    }
                }
            }

            (navBar as NavBar)?.PageAppearing();
        }
    }
}
