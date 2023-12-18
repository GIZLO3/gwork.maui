using CommunityToolkit.Mvvm.ComponentModel;
using gwork.maui.Data;
using gwork.maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        async void GetOffer()
        {
            if(OfferId != null)
            {
                var offerDatabase = new OfferDatabase();
                Offer = await offerDatabase.GetOfferAsync(int.Parse(OfferId));
            }
        }
    }
}
