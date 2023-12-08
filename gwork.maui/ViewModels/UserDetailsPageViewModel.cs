using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Services;

namespace gwork.maui.ViewModels
{
    public partial class UserDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        User user = (User)App.LoggedUser.Clone();

        [RelayCommand]
        async Task EditUser()
        {
            if (user != null)
            {
                var success = true;

                if (string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.Surname))
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wszytkie pola!", "OK");
                }
                else
                {
                    if (!user.Name.All(char.IsLetterOrDigit) || user.Name.Length > 256)
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Imię musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                    }
                    if (!user.Surname.All(char.IsLetterOrDigit) || user.Surname.Length > 256)
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Nazwisko musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                    }

                }

                if (success)
                {
                    App.LoggedUser.Name = user.Name;
                    App.LoggedUser.Surname = user.Surname;
                    var userDatabase = new UserDatabase();
                    await userDatabase.UdateUserAsync(App.LoggedUser);
                    JsonService.WriteFile(App.LoggedUser, App.LoggedUserJsonFilePath);
                }
            }
        }
    }
}
