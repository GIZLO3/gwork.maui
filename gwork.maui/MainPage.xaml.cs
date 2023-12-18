using gwork.maui.Controls;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Services;
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

            var userDatabase = new UserDatabase();

            var storedAdmin = await userDatabase.GetUserAsync("admin@admin");//sprawdzanie czy istnieje admin
            if( storedAdmin == null || !storedAdmin.IsAdmin)
            {
                var admin = new User();
                admin.Email = "admin@admin";
                admin.Password = PasswordService.HashPasword("12345678", out var salt);
                admin.Salt = salt;
                admin.IsAdmin = true;
                await userDatabase.InsertUserAsync(admin);
            }

            if (File.Exists(App.LoggedUserJsonFilePath))//zprawdzanie zalogowanego uzytkownika z pliku
            {
                StreamReader streamReader = new(App.LoggedUserJsonFilePath);
                var json = streamReader.ReadToEnd();
                var localUser = JsonSerializer.Deserialize<User>(json);
                streamReader.Close();

                if (localUser != null)
                {
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

            navBar.PageAppearing();

            if (BindingContext is MainPageViewModel mainPageViewModel)
                mainPageViewModel.GetOffers();
        }
    }
}
