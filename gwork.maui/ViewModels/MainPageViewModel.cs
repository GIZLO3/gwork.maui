using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using gwork.maui.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Offer>? offers;

        [ObservableProperty]
        private int offersCount;

        public async void GetOffers()
        {
            var offerDatabase = new OfferDatabase();
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
    }
}
