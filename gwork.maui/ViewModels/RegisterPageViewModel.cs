﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Services;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace gwork.maui.ViewModels
{
    public partial class RegisterPageViewModel : ObservableObject
    {
        private UserDatabase userDatabase = new UserDatabase();

        [ObservableProperty]
        private string? email, name, surname, phoneNumber, password, password2;

        [RelayCommand]
        private async Task Register()
        {
            var success = true;

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Password2))
            {
                success = false;
                await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wszytkie pola!", "OK");
            }
            else
            {
                if (!new EmailAddressAttribute().IsValid(Email))
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Adres email jest niepoprawny!", "OK");
                }
                if (!Name.All(char.IsLetterOrDigit) || Name.Length > 256)
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Imię musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                }
                if (!Surname.All(char.IsLetterOrDigit) || Surname.Length > 256)
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Nazwisko musi mieć od 1 do 256 znaków i może zawierać tylko litery i liczby!", "OK");
                }
                if(!Regex.IsMatch(PhoneNumber, @"^([0-9]{9})$"))
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Błędny numer telefonu", "OK");
                }
                if (Password.Length < 8)
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Hasło musi zawierać przynajmniej 8 znaków!","OK");
                }
                if (Password2 != Password)
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Hasła się nie zgadzają!", "OK");
                }
                

                var storedUser = await userDatabase.GetUserAsync(Email);//sprawdzanie czy podany email już istnieje
                if (storedUser != null)
                {
                    success = false;
                    await Shell.Current.DisplayAlert("Błąd", "Użytkownik z takim emailem już istnieje!", "OK");
                }
            }

            if (success)//dodanie do bazy uzytkownika
            {
                var user = new User();
                user.Email = Email;
                user.Name = Name;
                user.Surname = Surname;
                user.PhoneNumber = PhoneNumber;
                user.Password = PasswordService.HashPasword(Password, out var salt);
                user.Salt = salt;    
                await userDatabase.InsertUserAsync(user);

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}