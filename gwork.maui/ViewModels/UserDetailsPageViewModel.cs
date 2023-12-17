using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Pages;
using gwork.maui.Services;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace gwork.maui.ViewModels
{
    public partial class UserDetailsPageViewModel : ObservableObject
    {
        EmployeeDetailsDatabase employeeDetailsDatabase = new();

        [ObservableProperty]
        User user = (User)App.LoggedUser.Clone();

        [ObservableProperty]
        string? password, newPassword, newPasswordConfirmation;

        [ObservableProperty]
        EmployeeDetails employeeDetails = new();

        [ObservableProperty]
        List<string> educationEnumList = Enum.GetNames(typeof(EducationEnum)).ToList();

        [ObservableProperty]
        string? educationEnumSelected;

        public UserDetailsPageViewModel()
        {
            GetEmployeeDetails();
        }

        private async void GetEmployeeDetails()
        {
            var employeeDetails = await employeeDetailsDatabase.GetEmpleyeeDetailsAsync(User.DetailsId);

            if (employeeDetails != null)
            {
                EmployeeDetails = employeeDetails;
                EducationEnumSelected = Enum.GetName(typeof(EducationEnum), EmployeeDetails.Education);
            }
            else
                employeeDetails = new EmployeeDetails();
        }

        [RelayCommand]
        async Task EditUser()
        {
            if (User != null)
            {
                var success = true;
                var changePassword = false;

                if (string.IsNullOrEmpty(User.Name) || string.IsNullOrEmpty(User.Surname) || string.IsNullOrEmpty(User.PhoneNumber) || string.IsNullOrEmpty(Password))
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wymagane pola!", "OK");
                }
                else
                {
                    if (!User.Name.All(char.IsLetterOrDigit) || User.Name.Length > 256)
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Imię musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                    }
                    if (!User.Surname.All(char.IsLetterOrDigit) || User.Surname.Length > 256)
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Nazwisko musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                    }
                    if (!Regex.IsMatch(User.PhoneNumber, @"^([0-9]{9})$"))
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Błędny numer telefonu", "OK");
                    }
                    if (!PasswordService.VerifyPassword(Password, App.LoggedUser.Password, App.LoggedUser.Salt))
                    {
                        success = false;
                        await Shell.Current.DisplayAlert("Błąd", "Hasło jest błędne", "OK");
                    }

                    if (!string.IsNullOrWhiteSpace(NewPassword) && !string.IsNullOrWhiteSpace(NewPasswordConfirmation))
                    {
                        changePassword = true;

                        if (NewPassword.Length < 8)
                        {
                            success = false;
                            await Shell.Current.DisplayAlert("Błąd", "Hasło musi zawierać przynajmniej 8 znaków!", "OK");
                        }
                        if (NewPassword != NewPasswordConfirmation)
                        {
                            success = false;
                            await Shell.Current.DisplayAlert("Błąd", "Nowe hasła się nie zgadzają!", "OK");
                        }
                    }
                }

                if (success)
                {
                    App.LoggedUser = User;

                    if(changePassword)
                    {
                        App.LoggedUser.Password = PasswordService.HashPasword(NewPassword, out var salt);
                        App.LoggedUser.Salt = salt;
                    }

                    var userDatabase = new UserDatabase();
                    await userDatabase.UdateUserAsync(App.LoggedUser);
                    JsonService.WriteFile(App.LoggedUser, App.LoggedUserJsonFilePath);

                    await Shell.Current.GoToAsync("..");
                    await Shell.Current.GoToAsync(nameof(UserDetailsPage));
                }
            }
        }

        [RelayCommand]
        async Task EditEmployeeDetails()
        {
            var success = true;

            if (success)
            {
                if(EducationEnumSelected != null)
                    EmployeeDetails.Education = Enum.Parse<EducationEnum>(EducationEnumSelected);

                var detailsId = await employeeDetailsDatabase.SaveEmpleyeeDetailsAsync(EmployeeDetails);
                if(App.LoggedUser.DetailsId == 0)
                    App.LoggedUser.DetailsId = detailsId;

                var userDatabase = new UserDatabase();
                await userDatabase.UdateUserAsync(App.LoggedUser);
                JsonService.WriteFile(App.LoggedUser, App.LoggedUserJsonFilePath);

                await Shell.Current.GoToAsync("..");
                await Shell.Current.GoToAsync(nameof(UserDetailsPage));
            }
        }
    }
}
