using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Data;
using gwork.maui.Models;
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
        ObservableCollection<Offer> offers = new ObservableCollection<Offer>() 
        { 
            //new Offer() { PositionName = "Elektromechanik linii produkcyjnych", ConcractType = ConcractType.umowa_o_prace, Category = "cos", Description = "123sdbjfsjkhdfbjhksdjkfhsdjkfhsjldfjsdlhfkjsdlhfksjdfasdhfkjasdhfkljhsjfahslhdkfajhsbfnsjdkfnbsnfsdnlkfsn", ExpireDate = DateTime.Now.Date, SalaryLowest = 2800, SalaryHighest = 4500, Firm = new() { Name = "tego", Location= "Limanowa "} }
            new Offer() { PositionName = "Elektromechanik linii produkcyjnych", ConcractType = ConcractType.umowa_o_prace, Category = "cos", Description = "123sdbjfsjkhdfbjhksdjkfhsdjkfhsjldfjsdlhfkjsdlhfksjdfasdhfkjasdhfkljhsjfahslhdkfajhsbfnsjdkfnbsnfsdnlkfsn", ExpireDate = DateTime.Now.Date, SalaryLowest = 2800, SalaryHighest = 4500 }
        };

        public MainPageViewModel()
        {
            GetOffers();
        }
        public async void GetOffers()
        {
            var offerDatabase = new OfferDatabase();
            var offers = await offerDatabase.GetOffersAsync();
            if (offers != null)
                Offers = offers;
            else
                Offers = new();
        }
    }
}
