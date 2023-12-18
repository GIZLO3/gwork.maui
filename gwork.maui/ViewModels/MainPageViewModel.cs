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
        ObservableCollection<Offer>? offers;

        public async void GetOffers()
        {
            var offerDatabase = new OfferDatabase();
            var offers = await offerDatabase.GetOffersAsync();
            if (offers != null)
                Offers = offers;
            else
                Offers = new();
        }

        [RelayCommand]
        void GoToOffers()
        {
            Shell.Current.GoToAsync(nameof(AddOrEditOfferPage));
        }
    }
}
