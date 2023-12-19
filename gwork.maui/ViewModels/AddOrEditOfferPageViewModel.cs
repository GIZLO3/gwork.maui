using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;

namespace gwork.maui.ViewModels
{
    [QueryProperty(nameof(OfferId), nameof(OfferId))]
    public partial class AddOrEditOfferPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? offerId;

        [ObservableProperty]
        private Offer offer = new() { ExpireDate = DateTime.Now };

        [ObservableProperty]
        private List<string> concractTypeEnumList = Enum.GetNames(typeof(ConcractTypeEnum)).ToList();

        [ObservableProperty]
        private string concractTypeEnumSelected = "";

        [ObservableProperty]
        private string? mode;

        private OfferDatabase offerDatabase = new();

        partial void OnOfferIdChanged(string? value)
        {
            SetUpEditMode();
        }

        private async void SetUpEditMode()
        {
            Mode = "Edytuj ofertę";

            Offer = await offerDatabase.GetOfferAsync(int.Parse(OfferId));
            ConcractTypeEnumSelected = Enum.GetName(typeof(ConcractTypeEnum), Offer.ConcractType);
        }
        
        public AddOrEditOfferPageViewModel()
        { 
            Mode = "Dodaj ofertę";
        }

        [RelayCommand]
        private async Task AddOrEditOffer()
        {
            var success = true;

            if (string.IsNullOrEmpty(Offer.PositionName) || string.IsNullOrEmpty(ConcractTypeEnumSelected)  || string.IsNullOrEmpty(Offer.Category) || string.IsNullOrEmpty(Offer.Description) || string.IsNullOrEmpty(Offer.SalaryLowest.ToString()) || string.IsNullOrEmpty(Offer.SalaryHighest.ToString()) || string.IsNullOrEmpty(Offer.FirmName) || string.IsNullOrEmpty(Offer.FirmLocation))
            {
                success = false;
                await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wymagane pola!", "OK");
            }

            if(success)
            {
                Offer.ConcractType = Enum.Parse<ConcractTypeEnum>(ConcractTypeEnumSelected);
                await offerDatabase.SaveOfferAsync(Offer);

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}