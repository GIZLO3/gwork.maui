using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Pages;
using gwork.maui.Services;

namespace gwork.maui.ViewModels
{
    public partial class UserDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private User user = (User)App.LoggedUser.Clone();

        [ObservableProperty]
        private string? password, newPassword, newPasswordConfirmation;

        [ObservableProperty]
        private EmployeeDetails employeeDetails = new();

        [ObservableProperty]
        private List<string> educationEnumList = Enum.GetNames(typeof(EducationEnum)).ToList();

        [ObservableProperty]
        private string? educationEnumSelected;

        [ObservableProperty]
        private ObservableCollection<UserOfferApplyListItem>? appliedOffers;

        private EmployeeDetailsDatabase employeeDetailsDatabase = new();
        private UserOfferApplyDatabase userOfferApplyDatabase = new();
        private OfferDatabase offerDatabase = new();
        private UserDatabase userDatabase = new();

        public async void GetEmployeeDetails()
        {
            var employeeDetails = await employeeDetailsDatabase.GetEmpleyeeDetailsAsync(User.DetailsId);

            if (employeeDetails != null)
            {
                EmployeeDetails = employeeDetails;
                EducationEnumSelected = Enum.GetName(typeof(EducationEnum), EmployeeDetails.Education);
            }
        }

        public async void GetAppliedOffers()
        {
            var userOffersApplied = await userOfferApplyDatabase.GetUserOffersAppliedAsync(User.Id);

            var userOfferApplyListItems = new ObservableCollection<UserOfferApplyListItem>();
            foreach (var userOfferApply in userOffersApplied)
            {
                var userOfferApplyListItem = new UserOfferApplyListItem();
                userOfferApplyListItem.User = await userDatabase.GetUserAsync(userOfferApply.UserId);
                userOfferApplyListItem.Offer = await offerDatabase.GetOfferAsync(userOfferApply.OfferId);
                userOfferApplyListItem.Status = userOfferApply.Status;

                userOfferApplyListItems.Add(userOfferApplyListItem);
            }

            AppliedOffers = userOfferApplyListItems;
        }

        [RelayCommand]
        private async Task EditUser()
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

                        NewPassword = "";
                        NewPasswordConfirmation = "";
                    }

                    var userDatabase = new UserDatabase();
                    await userDatabase.UdateUserAsync(App.LoggedUser);
                    JsonService.WriteFile(App.LoggedUser, App.LoggedUserJsonFilePath);
                    Password = "";

                    await Shell.Current.DisplayAlert("Informacja", "Pomyślnie edytowano dane", "OK");
                }
            }
        }

        [RelayCommand]
        private async Task EditEmployeeDetails()
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

                await Shell.Current.DisplayAlert("Informacja", "Pomyślnie edytowano dane", "OK");
            }
        }

        [RelayCommand]
        private async Task GoToOfferDetails(object commandParameter)
        {
            var offer = commandParameter as Offer;
            if (offer != null)
            {
                await Shell.Current.GoToAsync($"{nameof(OfferDetailsPage)}?OfferId={offer.Id}");
            }
        }

        [RelayCommand]
        private async Task DeleteUserOfferApply(object commandParameter)
        {
            var offer = commandParameter as Offer;
            if (offer != null)
            {
                var result = await Shell.Current.DisplayAlert("Ostrzeżenie", $"Czy na pewno anulować: {offer.PositionName}?", "Tak", "Nie");
                if (result)
                {
                    var userOfferApply = await userOfferApplyDatabase.GetUserOfferApplyAsync(User.Id, offer.Id);
                    if(userOfferApply != null)
                        await userOfferApplyDatabase.DeleteUserOfferApplyAsync(userOfferApply);

                    GetAppliedOffers();
                }
            }
        }
    }
}