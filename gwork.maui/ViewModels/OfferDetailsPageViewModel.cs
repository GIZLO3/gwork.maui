using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;

namespace gwork.maui.ViewModels
{
    [QueryProperty(nameof(OfferId), nameof(OfferId))]
    public partial class OfferDetailsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? offerId;

        [ObservableProperty]
        private Offer? offer;

        partial void OnOfferIdChanged(string value)
        {
            GetOffer();
        }
        private async void GetOffer()
        {
            if(OfferId != null)
            {
                var offerDatabase = new OfferDatabase();
                Offer = await offerDatabase.GetOfferAsync(int.Parse(OfferId));
            }
        }

        [RelayCommand]
        private async Task ApplyForOffer()
        {
            if(App.LoggedUser != null)
            {
                if(Offer != null)
                {
                    var userOfferApplyDatabase = new UserOfferApplyDatabase();

                    var storedUserOfferApply = await userOfferApplyDatabase.GetUserOfferApplyAsync(App.LoggedUser.Id, Offer.Id);
                    if(storedUserOfferApply == null)
                    {
                        var userOfferApply = new UserOfferApply();
                        userOfferApply.UserId = App.LoggedUser.Id;
                        userOfferApply.OfferId = Offer.Id;

                        await userOfferApplyDatabase.SaveUserOfferApplyAsync(userOfferApply);
                        await Shell.Current.DisplayAlert("Informacja", "Pomyślnie aplikowano o pracę", "OK");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Błąd", "Już aplikowałeś o tą pracę", "OK");
                    }
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Błąd", "Zaloguj się aby aplikować o pracę", "OK");
            }
        }
    }
}