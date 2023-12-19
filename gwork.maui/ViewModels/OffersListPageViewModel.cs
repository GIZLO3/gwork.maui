using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Pages;
using System.Collections.ObjectModel;

namespace gwork.maui.ViewModels
{
    public partial class OffersListPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Offer>? offers;

        private OfferDatabase offerDatabase = new();

        public async void GetOffers()
        {
            var offers = await offerDatabase.GetOffersAsync();
            if (offers != null)
                Offers = offers;
            else
                Offers = new();
        }

        [RelayCommand]
        private async Task GoToOfferEditPage(object commandParameter)
        {
            var offer = commandParameter as Offer;
            if (offer != null)
            {
                await Shell.Current.GoToAsync($"{nameof(AddOrEditOfferPage)}?OfferId={offer.Id}");
            }
        }

        [RelayCommand]
        private async Task DeleteOffer(object commandParameter)
        {
            var offer = commandParameter as Offer;
            if (offer != null)
            {
                var result = await Shell.Current.DisplayAlert("Ostrzeżenie", $"Czy na pewno chcesz usunąć ofertę nr: {offer.Id}?", "Tak", "Nie");
                if (result)
                {
                    await offerDatabase.DeleteOfferAsync(offer);
                    GetOffers();
                }
            }
        }
    }
}