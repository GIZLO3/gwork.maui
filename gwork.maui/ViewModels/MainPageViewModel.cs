using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Pages;
using System.Collections.ObjectModel;

namespace gwork.maui.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Offer>? offers;

        [ObservableProperty]
        private int offersCount;

        [ObservableProperty]
        private string? searchBarText;

        private OfferDatabase offerDatabase = new();

        public async void GetOffers()
        {
            
            var offers = await offerDatabase.GetOffersAsync();
            if (offers != null)
                Offers = offers;
            else
                Offers = new();

            OffersCount = Offers.Count();
        }

        [RelayCommand]
        private async Task GoToOfferDetails(object commandParameter)
        {
            var offer = commandParameter as Offer;
            if(offer != null)
            {
                await Shell.Current.GoToAsync($"{nameof(OfferDetailsPage)}?OfferId={offer.Id}");
            }
        }

        [RelayCommand]
        private async Task SearchOffer(object commandParameter)
        {
            var searchText = (string)commandParameter;
            if (string.IsNullOrEmpty(searchText))
            {
                GetOffers();
            }
            else
            {
                var offers = await offerDatabase.GetOffersAsyncBySearchText(searchText.ToLower());
                if (offers != null)
                    Offers = offers;
                else
                    Offers = new();
            }
        }
    }
}