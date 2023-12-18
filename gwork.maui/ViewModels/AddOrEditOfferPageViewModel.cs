using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.ViewModels
{
    public partial class AddOrEditOfferPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string? positionName, category, description, firmName, firmLocation;

        [ObservableProperty]
        private DateTime expireDate = DateTime.Now;

        [ObservableProperty]
        private decimal salaryLowest, salaryHighest;

        [ObservableProperty]
        private List<string> concractTypeEnumList = Enum.GetNames(typeof(ConcractTypeEnum)).ToList();

        [ObservableProperty]
        private string concractTypeEnumSelected = "";

        [ObservableProperty]
        private string? mode;

        private readonly bool EditMode;
        public AddOrEditOfferPageViewModel()
        {
            EditMode = false;
            Mode = "Dodaj ofertę";
        }

        [RelayCommand]
        private async Task AddOrEditOffer()
        {
            var success = true;

            if (string.IsNullOrEmpty(PositionName) || string.IsNullOrEmpty(ConcractTypeEnumSelected)  || string.IsNullOrEmpty(Category) || string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(SalaryLowest.ToString()) || string.IsNullOrEmpty(SalaryHighest.ToString()) || string.IsNullOrEmpty(FirmName) || string.IsNullOrEmpty(FirmLocation))
            {
                success = false;
                await Shell.Current.DisplayAlert("Błąd", "Uzupełnij wymagane pola!", "OK");
            }

            if (EditMode && success)
            {

            }
            else if(success)
            {
                var offer = new Offer();
                offer.PositionName = PositionName;
                offer.ConcractType = Enum.Parse<ConcractTypeEnum>(ConcractTypeEnumSelected);
                offer.ExpireDate = ExpireDate;
                offer.Category = Category;
                offer.Description = Description;
                offer.SalaryLowest = SalaryLowest;
                offer.SalaryHighest = SalaryHighest;
                offer.FirmName = FirmName;
                offer.FirmLocation = FirmLocation;

                var offerDatabase = new OfferDatabase();
                await offerDatabase.InsertOfferAsync(offer);
                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
