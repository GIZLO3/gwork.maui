using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using gwork.maui.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gwork.maui.ViewModels
{
    public partial class AdminPanelPageViewModel : ObservableObject
    {
        [RelayCommand]
        private void GoToAddOfferPage()
        {
            Shell.Current.GoToAsync(nameof(AddOrEditOfferPage));
        }
    }
}
