using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Services;

namespace gwork.maui.ViewModels
{
    public partial class LogInPageViewModel : ObservableObject
    {
        private UserDatabase userDatabase = new UserDatabase();

        [ObservableProperty]
        private string? email, password;

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wszytkie pola!", "OK");
            }
            else
            {
                var storedUser = await userDatabase.GetUserAsync(Email);//uzytkownik z tym samym emailem co podany
                if (storedUser != null && PasswordService.VerifyPassword(Password, storedUser.Password, storedUser.Salt))
                {
                    App.LoggedUser = storedUser;
                    JsonService.WriteFile(App.LoggedUser, App.LoggedUserJsonFilePath);
                    await Shell.Current.GoToAsync("..");
                }
                else
                    await Shell.Current.DisplayAlert("Błąd", "Login lub hasło jest niepoprawne!", "OK");
            }
        }
    }
}